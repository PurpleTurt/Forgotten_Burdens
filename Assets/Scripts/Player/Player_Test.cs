using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.UIElements;

public class Player_Test : MonoBehaviour
{
    //Number Values
    [SerializeField]
    private float speed;
    private float InputY;
    private float InputX;

    private Vector2 _Angle;
    private int _Ground_Type;

    private Rigidbody2D Player_RB;
    private Animator _Animator;

    //TileMap
    [SerializeField]
    private Tilemap Main_Tile_Map;
    //Tilemap Data
    private TileBase Tile_You_Are_On;
    private Tile_Data_Manager Tile_Data_Manager;
    //Audio
    [SerializeField]
    private AudioSource[] audioSources;
    [SerializeField]
    private GameObject[] Effects;



    void Start()
    {

        Player_RB = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        Tile_Data_Manager = FindFirstObjectByType<Tile_Data_Manager>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        InputY = Input.GetAxisRaw("Vertical");
        InputX = Input.GetAxisRaw("Horizontal");
        //Animator
        _Animator.SetFloat("Y",InputY);
        _Animator.SetFloat("X", Mathf.Abs(InputX));



        Player_RB.linearVelocityY = ((InputY - _Angle.x * InputX) + (_Angle.y * InputY * 0.4f)) * Time.deltaTime * speed;
        Player_RB.linearVelocityX = (InputX) * Time.deltaTime * speed;




        //Gets The Current Tile You are Standing On
        Vector3Int Grid_Pos = Main_Tile_Map.WorldToCell(transform.position);
        Tile_You_Are_On = Main_Tile_Map.GetTile(Grid_Pos);


         if (Tile_Data_Manager._DataFromTiles.ContainsKey(Tile_You_Are_On))
            {
                _Angle = Tile_Data_Manager._DataFromTiles[Tile_You_Are_On].Angle_Dir;
                _Ground_Type = Tile_Data_Manager._DataFromTiles[Tile_You_Are_On].Ground_Type;
            }
         else { _Angle = Vector2.zero; _Ground_Type = 0; }
        
            
        
    }




    //Audio
    public void Step_Audio()
    {

        audioSources[_Ground_Type].pitch = Random.Range(0.8f, 1.2f);
        audioSources[_Ground_Type].Play();
        if (_Ground_Type == 0)
        {
            Instantiate(Effects[_Ground_Type]).transform.position = transform.position;
        }
    }


}
