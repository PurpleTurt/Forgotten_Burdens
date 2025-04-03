using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Tilemap inventory_Background;



    public Sprite[] Objects_Sprites;
    [SerializeField]
    private GameObject[] Slots;
    [SerializeField]
    private Tile Inventory_Tile_Type;


    [SerializeField]
    private GameObject Curser;
    [Range(0,27)]
    public int Current_Slot = 0;

    private Vector2 input;




    private void Start()
    {
        Curser.transform.position = Slots[Current_Slot].transform.position;

       

       
        


    }


    public void Pause_Press()
    {
        

        inventory_Background.gameObject.SetActive(!inventory_Background.gameObject.activeSelf);
    }
    public void Movement_input(InputAction.CallbackContext reference)
    {
        input = new Vector2(reference.ReadValue<Vector2>().normalized.x,reference.ReadValue<Vector2>().normalized.y * 10);


            Current_Slot = Current_Slot + Mathf.RoundToInt(input.x);
            Current_Slot %= Slots.Length;
            Curser.transform.position = Slots[Current_Slot].transform.position;
        


        
        

    }


}
