
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;


public class Player_State_Machine : MonoBehaviour
{
    //States
    private Player_Base_State Current_State;
    public Player_Walking_State State_Walking = new Player_Walking_State();
    public Player_Idle_State State_Idle = new Player_Idle_State();

    //Audio
    [SerializeField]
    private AudioSource[] audioSources;
    [SerializeField]
    private int Amount_of_Audio_Sources;

    //Effects
    [SerializeField]
    private GameObject[] Effects;
    [SerializeField]
    private int Amount_of_Effects;

    //Bools
    public bool Is_Left;

    //Values
    public Vector2 input;
    public float Speed;

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

        

        //Start State (ALWAYS do this Second)
        Current_State = State_Walking;
        Current_State.On_State_Enter(this);

        
    }
    //Flips the Object
    public void Flip()
    {
        Is_Left = !Is_Left;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;

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
        if (_Ground_Type <= Amount_of_Audio_Sources) 
        {
            audioSources[_Ground_Type].pitch = Random.Range(0.8f, 1.2f);
            audioSources[_Ground_Type].Play();
        }
        //Checks if there are enough effects in the array to Instantiate
        if (_Ground_Type <= Amount_of_Effects)
        {
            Instantiate(Effects[_Ground_Type]).transform.position = transform.position;
        }
    }


}
