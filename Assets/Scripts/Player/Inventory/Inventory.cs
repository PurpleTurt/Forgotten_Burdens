using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Tilemap inventory_Background;

    //player
    [SerializeField]
    private GameObject player;

    
    [SerializeField]
    private GameObject[] Slots;

    public static int[] Items_in_Slots = {0,};


    [SerializeField]
    private Items_List items_List;



    [SerializeField]
    private GameObject Curser;
    [Range(1,28)]
    public int Current_Slot = 0;

    private Vector2 input;
    



    private void Start()
    {
        Curser.transform.position = Slots[Current_Slot].transform.position;

        


        Slots[0].GetComponent<SpriteRenderer>().sprite = items_List.Items[1].GetComponent<IEquipable_Item>().inventory_sprite();
        
        
       
        


    }


    public void Pause_Press()
    {
        

        inventory_Background.gameObject.SetActive(!inventory_Background.gameObject.activeSelf);
    }
    public void Movement_input(InputAction.CallbackContext reference)
    {

        
        if (input == Vector2.zero)
        {  
        
        //Moves the curser
        input = new Vector2(reference.ReadValue<Vector2>().x,reference.ReadValue<Vector2>().y * 7);

            if(Current_Slot + Mathf.RoundToInt(input.x) - Mathf.RoundToInt(input.y) < 0)
            {
                Current_Slot += Mathf.RoundToInt(input.x) - Mathf.RoundToInt(input.y) + 28;
            }
            else
            {
             Current_Slot += Mathf.RoundToInt(input.x) - Mathf.RoundToInt(input.y);
            }
            Current_Slot %= 28;
            Curser.transform.position = Slots[Current_Slot].transform.position;
        }
        
        
        
        

    }


}
