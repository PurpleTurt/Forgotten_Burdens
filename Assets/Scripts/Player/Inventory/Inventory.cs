using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory_Background;



    
    [SerializeField]
    private GameObject[] Slots;

    public static int[] Items_in_Slots = {0,1,1,1,2,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,1,0,0,0,0,0,0,0};


    [SerializeField]
    private Items_List items_List;

    //Equip Slots in UI
    [SerializeField]
    private UnityEngine.UI.Image[] Buttons_Images; 

    [SerializeField]
    private GameObject Curser;
    [Range(1,28)]
    public int Current_Slot = 0;

    private Vector2 input;

    //player
    private Player_State_Machine player;

    //Audio Clips
    [SerializeField]
    private AudioClip[] Audio_clips;
    [SerializeField]
    private AudioSource AudioSource;
    



    private void Start()
    {
        Curser.transform.position = Slots[Current_Slot].transform.position;
        //Puts items in respective slots decided by "Items_in_Slots[]"
        int index = 0;
        foreach(var Slot in Slots)
        {
            UnityEngine.UI.Image slot_image = Slot.GetComponent<UnityEngine.UI.Image>();
            if(items_List.Items[Items_in_Slots[index]] != null)
            {
            slot_image.sprite = items_List.Items[Items_in_Slots[index]].GetComponent<IEquipable_Item>().inventory_sprite();
            slot_image.color = Color.white;
            }
            else { slot_image.color = Color.clear; }
            index++;
        }

        
        //Gets player
        player = GetComponent<Player_State_Machine>();
       
        //Changes Icon For Item Equips
        Change_Item_Button_Icon(0);

        //Loads Audio Data
        foreach(var clip in Audio_clips)
        {
            clip.LoadAudioData();
        }

    }


    public void Pause_Press(InputAction.CallbackContext context)
    {
        //Puts items in respective slots decided by "Items_in_Slots[]"
        if(context.ReadValue<float>() == 0)
        {
        int index = 0;
        foreach(var Slot in Slots)
        {
            UnityEngine.UI.Image slot_image = Slot.GetComponent<UnityEngine.UI.Image>();
            if(items_List.Items[Items_in_Slots[index]] != null)
            {
            slot_image.sprite = items_List.Items[Items_in_Slots[index]].GetComponent<IEquipable_Item>().inventory_sprite();
            slot_image.color = Color.white;
            }
            else { slot_image.color = Color.clear; }
            index++;
        }
        //Pauses the game and enables the inventory
        inventory_Background.SetActive(!inventory_Background.activeSelf);
        if(inventory_Background.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else {Time.timeScale = 1;}
        
        AudioSource.clip = Audio_clips[0];
        AudioSource.Play();
        }

    }

    public void Equip_Item_To_Slot_One(InputAction.CallbackContext context)
    {
        if(inventory_Background.activeSelf == true && context.ReadValue<float>() == 0)
        {
         
                Player_State_Machine.E_Key_Equip = Items_in_Slots[Current_Slot];
                //Changes Icon For Item Equips
                Change_Item_Button_Icon(0);

                

                //Destroys Current Item in hand
                Destroy(player.Item_in_Hand);
                player.Item_in_Hand = null;

                //Plays curser select animation
                Curser.GetComponent<Animator>().Play("Curser_Select",0,0);
                AudioSource.clip = Audio_clips[1];
                AudioSource.Play();

                
            

                
            

        }

    }

    //Changes the item displayed on the equip buttons
    private void Change_Item_Button_Icon(int Button_ID)
    {
                if(Player_State_Machine.E_Key_Equip == 0)
                {
                    Buttons_Images[Button_ID].color = Color.clear;
                }
                else
                {
                    Buttons_Images[Button_ID].sprite = items_List.Items[Player_State_Machine.E_Key_Equip].GetComponent<IEquipable_Item>().inventory_sprite();
                    Buttons_Images[Button_ID].color = Color.white;
                }
    }

    public void Movement_input(InputAction.CallbackContext reference)
    {

        if(inventory_Background.activeSelf == true && Curser.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != "Curser_Select"){
 
        
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
