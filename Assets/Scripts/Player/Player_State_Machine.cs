
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;






public class Player_State_Machine : MonoBehaviour , IDay_Cycle_Effected,ISave_Data
{
    //States
    private Player_Base_State Current_State;
    public Player_Running_State State_Running = new Player_Running_State();
    public Player_Idle_State State_Idle = new Player_Idle_State();
    public Player_Roll_State State_Roll = new Player_Roll_State();
    public Player_Walking_State State_Walking = new Player_Walking_State();

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
    public int E_Key_Equip;



    //Items List
    public Items_List Items;

    //Bools
    public bool Locked_Controls;
    public bool Can_Roll;
    //Treated Like a bool
    // 0 = false; 1 = true;
    //Has to be this way because of the way I set up inputs
    public float Is_Walking;
    

    //Values
    public Vector2 input;
    [Range (1,4)]
    public int Last_Input_Dir_Index = 1;
    public static Vector2 Last_Input_Dir;
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
                transform.position = Spawn.transform.position;

            }

        }
        //Gets timemap
        Main_Tile_Map = GameObject.Find("Main_Tilemap_Layer_1").GetComponent<Tilemap>();

        

        

        //Start State (ALWAYS do this Second)
        Current_State = State_Idle;
        Current_State.On_State_Enter(this);

        
    }


    
    public void State_Switch(Player_Base_State New_State)
    {
        Current_State.On_State_Exit(this);
        Current_State = New_State;
        Current_State.On_State_Enter(this);

    }
    //Gets input
    public void On_Input(InputAction.CallbackContext Context)
    {
        if (Locked_Controls == false)
        {
            input = Context.ReadValue<Vector2>();
        }
        
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

    //Item on first equip slot
    public void item_one_use(InputAction.CallbackContext context)
    {
        if(Current_State != State_Roll && Items.Items[E_Key_Equip] != null)
        {
            
            //Puts item in hand if it isn't already
            if(Item_in_Hand == null || Item_in_Hand != PrefabUtility.IsPartOfPrefabAsset(Items.Items[E_Key_Equip]))
            {
            Item_in_Hand = Instantiate(Items.Items[E_Key_Equip]);

            }
            else
            {
            //Uses the item
            Item_in_Hand.GetComponent<IEquipable_Item>().On_Item_Use(context.ReadValue<float>());
            }
        }

    }
    //Puts item in hand away when called
    public void put_item_in_hand_away()
    {
        Destroy(Item_in_Hand);
        Item_in_Hand = null;
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
            Audio_Source.pitch = Random.Range(0.8f, 1.2f);
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

    //Load Zone Manager
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "load_zone") 
        { 
            Load_Zone load_Zone = collision.GetComponent<Load_Zone>();
            if (load_Zone != null) 
            {
                Load_Zone_ID = load_Zone.Load_Zone_ID;
                Save_Manager.instance.Save_Game();
                SceneManager.LoadSceneAsync(load_Zone.scene_To_Load_To);

            }
        
        }
    }

    //Switches object in hands sorting layer to apear behind the player when walking
    public void Animator_Item_In_Hand_Layer_Switch(int layer)
    {
        if(Item_in_Hand != null)
        {
            Item_in_Hand.GetComponent<SpriteRenderer>().sortingOrder = layer;
        }

    }

    //Exits Roll state. Called from animator
    public void Enter_Idle_Animation()
    {
        State_Switch(State_Idle);

    }





}
