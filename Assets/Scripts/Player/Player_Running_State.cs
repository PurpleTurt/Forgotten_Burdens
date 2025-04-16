using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Running_State : Player_Base_State
{
    public override IEnumerator enumerator(Player_State_Machine player)
    {
        throw new System.NotImplementedException();
    }

    public override void On_State_Enter(Player_State_Machine player)
    {
        player.Player_Anim.Play("Run",0,0);
    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
            //Movement
            player.Player_RB.linearVelocityY = ((player.input.y - player._Angle.x * player.input.x) + (player._Angle.y * player.input.y * 0.4f)) * Time.deltaTime * player.Speed;
            player.Player_RB.linearVelocityX = (player.input.x) * Time.deltaTime * player.Speed;
        
        


        //Checks For state Change to Walking State
        if (Mathf.Abs(player.input.x) < 0.5f && Mathf.Abs(player.input.y) < 0.5f || player.Is_Walking == 1)
        { player.State_Switch(player.State_Walking); }
        

        //Checks for a state change
        if (player.input == Vector2.zero) { player.State_Switch(player.State_Idle); }
        else 
        {
            //Animator
            
                player.Player_Anim.SetFloat("X", Mathf.Round(player.input.normalized.x) + Mathf.Round(Mathf.Abs(player.input.normalized.y) *-1 * Mathf.Round(player.input.normalized.x)));
                player.Player_Anim.SetFloat("Y", Mathf.Round(player.input.normalized.y));

            //Last input dir
            Player_State_Machine.Last_Input_Dir = player.input;
            



        }
        

    }
    //Item one use
    public override void on_item_one_use(Player_State_Machine player)
    {
        //Puts item in hand if it isn't already
        if(player.Item_in_Hand != player.Items.Items[0])
        {
            player.Item_in_Hand = player.Items.Items[0];

        }
        else
        {
        //Uses the item
        player.Items.Items[0].GetComponent<IEquipable_Item>().On_Item_Use();
        }
        

    }
    
}
