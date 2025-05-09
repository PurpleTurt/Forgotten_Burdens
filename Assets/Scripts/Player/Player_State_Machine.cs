
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using System;






public class Player_State_Machine : MonoBehaviour , IDay_Cycle_Effected,ISave_Data
{
    //States
    private Player_Base_State Current_State;
    public Player_Running_State State_Running = new Player_Running_State();
    public Player_Idle_State State_Idle = new Player_Idle_State();
    public Player_Roll_State State_Roll = new Player_Roll_State();
    public Player_Walking_State State_Walking = new Player_Walking_State();
    public Player_Attacking_State State_Attacking = new Player_Attacking_State();

    //Audio
    [SerializeField]
    private Audio_Bank Player_Audio_Bank;
    private int Amount_Of_Audio_Clips = 0;
    public AudioSource Audio_Source;

    //Effects
    [SerializeField]
    private GameObject[] Effects;
    [SerializeField]
    private int Amount_of_Effects;

    //Current Item in hand
    public GameObject Item_in_Hand;
    public GameObject item_in_hand_pos;

    //Equip Slots Data
    public static int[] Key_Equips = {0,0,0,0};



    //Items List
    public Items_List Items;

    //Bools

    public bool Can_Roll;

    public bool Locked_Controls = false;
    //Treated Like a bool
    // 0 = false; 1 = true;
    //Has to be this way because of the way I set up inputs
    public float Is_Walking;
    

    //Values
    public Vector2 input;
    [Range (1,4)]
    public int Last_Input_Dir_Index = 1;
    public static Vector2 Last_Input_Dir = new Vector2(0,-1);
    public float Speed;
    public int Health;
    public static int Load_Zone_ID;
    public static Dictionary<bool,string> Inventory_Flags;

    //Components
    public Rigidbody2D Player_RB;
    public Animator Player_Anim;





    //Tilemap Data
    private TileBase Tile_You_Are_On;
    private Tile_Data_Manager Tile_Data_Manager;
    //TileMap
    [SerializeField]
    private Tilemap Main_Tile_Map;
    //Tile Properties
    public Vector2 _Angle;
    public int _Ground_Type;

    void Start()
    {

        //Gets Components (ALWAYS Do this first)
        Player_RB = GetComponent<Rigidbody2D>();
        Player_Anim = GetComponent<Animator>();
        Tile_Data_Manager = FindFirstObjectByType<Tile_Data_Manager>();
        //Sets up audio bank
        foreach(AudioClip Clip in Player_Audio_Bank.audioClips)
        {
            Clip.LoadAudioData();
            Amount_Of_Audio_Clips++;

        }
        //Sets Spawn Position Based on Load zone ID
        Spawn_Point[] spawn_Points = FindObjectsByType<Spawn_Point>(FindObjectsSortMode.None); 
        foreach(Spawn_Point Spawn in spawn_Points)
        {
            if(Spawn.Spawn_ID == Load_Zone_ID)
            {
                //Sets position
                transform.position = Spawn.transform.position;
                //Sets Movement Dir and Starts scene enter Coroutine
                StartCoroutine(Scene_Load_Psuedo_Cutscene(Spawn.Dir_To_Move_To));

            }

        }
        //Gets timemap
        Main_Tile_Map = GameObject.Find("Main_Tilemap_Layer_1").GetComponent<Tilemap>();

        

        

        //Start State (ALWAYS do this Second)
        Current_State = State_Idle;
        Current_State.On_State_Enter(this);

        
    }
    //Roll Animation Stuff like state switching
    public void Roll_State_Switch_Animator_Stuff(){ State_Switch(State_Idle);}

    
    public void State_Switch(Player_Base_State New_State)
    {
        Current_State.On_State_Exit(this);
        Current_State = New_State;
        Current_State.On_State_Enter(this);

    }
    //Gets input
    public void On_Input(InputAction.CallbackContext Context)
    {
            if(Locked_Controls == false)
            input = Context.ReadValue<Vector2>();
        
        
    }
    public void On_Roll_Input() 
    { 
        if(Can_Roll == true) { State_Switch(State_Roll); }
    }

    //Gets Shift input
    public void On_Walk_State_input(InputAction.CallbackContext context)
    {
        Is_Walking = context.ReadValue<float>();
        
        
    }
    public void Main_Item_Use(InputAction.CallbackContext context)
    {

    }

    public void Item_One_Use(InputAction.CallbackContext context)
    {
        
        if(Current_State != State_Roll && Items.Items[Key_Equips[1]] != null && Time.timeScale == 1 && context.ReadValue<float>() == 1)
        StartCoroutine(item_use(1));
    }
    public void Item_Two_Use(InputAction.CallbackContext context)
    {

    }
    public void Item_Three_Use(InputAction.CallbackContext context)
    {
        
    }

    //Uses Item. To be call from input methods
    public IEnumerator item_use(int Button_ID)
    {
        
        
            
            //Puts item in hand if it isn't already
            if(Item_in_Hand == null || Item_in_Hand.GetComponent<IEquipable_Item>().Name() != Items.Items[Key_Equips[Button_ID]].GetComponent<IEquipable_Item>().Name())
            {
            //Plays Pullout Animation
            Player_Anim.Play(Items.Items[Key_Equips[Button_ID]].GetComponent<IEquipable_Item>().Pullout_Item_Animation(),0,0);

            //Destroys Current Item in hand
            Destroy(Item_in_Hand);
            Item_in_Hand = null;
            //Waits to instantiate item so it spawns during the animation instead of before
            yield return new WaitForSecondsRealtime(0.1f);
            //Gets Animation Clip Name
            string Pullout_Ani_Name = Player_Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            Item_in_Hand = Instantiate(Items.Items[Key_Equips[Button_ID]]);
            //Waits for Pullout animation to end
            yield return new WaitWhile(() => Player_Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == Pullout_Ani_Name);
            
            
            State_Switch(State_Idle);

            }
            else
            {
            //Uses the item
            Item_in_Hand.GetComponent<IEquipable_Item>().On_Item_Use(this);
            }
        

    }
    //Puts item in hand away when called
    public void put_item_in_hand_away()
    {
        if(Time.timeScale == 1)
        {
        Destroy(Item_in_Hand);
        Item_in_Hand = null;
        }
    }

    void Update()
    {

        Current_State.State_Update(this);

        //Gets the Current Tile Every Frame
        Get_Tile();
        //updates item in hands position to actually be in hand
        if(Item_in_Hand != null)
        {

        Item_in_Hand.transform.position = item_in_hand_pos.transform.position;

        }

        



        
    }



    void Get_Tile()
    {
        //Gets The Current Tile You are Standing On
        Vector3Int Grid_Pos = Main_Tile_Map.WorldToCell(transform.position);
        Tile_You_Are_On = Main_Tile_Map.GetTile(Grid_Pos);

        /*
         * Checks if the Current Tile you are on Has a Entry in 
         * the dictionary, If not, Sets possible values to 0
        */
        if (Tile_You_Are_On != null) {

            if (Tile_Data_Manager._DataFromTiles.ContainsKey(Tile_You_Are_On))
            {
                _Angle = Tile_Data_Manager._DataFromTiles[Tile_You_Are_On].Angle_Dir;
                _Ground_Type = Tile_Data_Manager._DataFromTiles[Tile_You_Are_On].Ground_Type;
            }
            
        }
        else { _Angle = Vector2.zero; _Ground_Type = 0; }
    }

    //Stepping Audio for Animator
    public void Step_Audio()
    {
        //Checks if there are enough audio sources in the array to play
        if (Amount_Of_Audio_Clips >= _Ground_Type) 
        {
            Audio_Source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            Audio_Source.clip = Player_Audio_Bank.audioClips[_Ground_Type];
            Audio_Source.Play();
        }
        //Checks if there are enough effects in the array to Instantiate
        if (_Ground_Type <= Amount_of_Effects)
        {
            Instantiate(Effects[_Ground_Type]).transform.position = transform.position;
        }
    }

    public void on_Daytime()
    {
        
    }

    public void on_Nightime()
    {
   
    }



    //Save Data Manager
    public void Save(ref Game_Data Data)
    {
        Data.Player_Health = Health;


    }

    public void load(Game_Data Data)
    {
        Health = Data.Player_Health;

    }




    private IEnumerator Scene_Load_Psuedo_Cutscene(Vector2 Dir_To_Walk_Towards)
    {

        Locked_Controls = true;

        
        UnityEngine.UI.Image image = GameObject.Find("Transition").GetComponent<UnityEngine.UI.Image>();
        //Gets Audio Sources for lerping
        AudioSource Ambience = GameObject.Find("Ambience_Source").GetComponent<AudioSource>();
        AudioSource music = GameObject.Find("Scene_Music").GetComponent<AudioSource>();
        while(image.color != Color.clear)
        {
            //Sets Player input
            input = Dir_To_Walk_Towards * 0.5f;
            //Lerps Audio volume to 0
            music.volume = Mathf.Lerp(music.volume,0.5f,Time.fixedDeltaTime);
            Ambience.volume = Mathf.Lerp(music.volume,0.5f,Time.fixedDeltaTime);
            
            
            yield return null;
        }

        
        Locked_Controls = false;
        input = Vector2.zero;
    }







}
