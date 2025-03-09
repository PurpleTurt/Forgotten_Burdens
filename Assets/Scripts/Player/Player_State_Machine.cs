
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


public class Player_State_Machine : MonoBehaviour , IDay_Cycle_Effected
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

    //Bools
    public bool Is_Left;
    //Treated Like a bool
    // 0 = false; 1 = true;
    //Has to be this way because of the way I set up inputs
    public float Is_Walking;
    

    //Values
    public Vector2 input;
    public Vector2 Last_Input_Dir;
    public float Speed;

    //Components
    public Rigidbody2D Player_RB;
    public Animator Player_Anim;



    //This is just a test, Delete when the Lamp object has been implemented
    [SerializeField]
    private Light2D Lamp;


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
        input = Context.ReadValue<Vector2>();
    }
    public void On_Roll_Input() 
    { 
        if(Current_State == State_Running) { State_Switch(State_Roll); }
    }
    //Gets Shift input
    public void On_Walk_State_input(InputAction.CallbackContext context)
    {
        Is_Walking = context.ReadValue<float>();
        
    }

    void FixedUpdate()
    {
        Current_State.State_Update(this);
        //Gets the Current Tile Every Frame
        Get_Tile();

        
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
        Lamp.enabled = false;
    }

    public void on_Nightime()
    {
        Lamp.enabled = true;
    }
}
