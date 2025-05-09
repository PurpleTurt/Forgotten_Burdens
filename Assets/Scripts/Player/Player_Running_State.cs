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
        //plays player Animation
        player.Player_Anim.Play("Run",0,0);
        player.Can_Roll = true;
        //Sets Item is hand floats if animator is present
        if(player.Item_in_Hand != null){
        //Gets Items Animator
        Animator Item_in_Hand_animator = player.Item_in_Hand.GetComponent<Animator>();

        if(Item_in_Hand_animator != null)
        {
            //Sets Floats and Plays Animation
            Item_in_Hand_animator.Play("Walking",0,0);
            Item_in_Hand_animator.SetFloat("X",player.Player_Anim.GetFloat("X"));
            Item_in_Hand_animator.SetFloat("Y",player.Player_Anim.GetFloat("Y"));

        }
        }
    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
            //Movement
            player.Player_RB.linearVelocityY = ((player.input.y - player._Angle.x * player.input.x) + (player._Angle.y * player.input.y * 0.4f)) * player.Speed * Time.fixedDeltaTime;
            player.Player_RB.linearVelocityX = (player.input.x) * player.Speed * Time.fixedDeltaTime;
        
        


        //Checks For state Change to Walking State
        if (Mathf.Abs(player.input.x) < 0.5f && Mathf.Abs(player.input.y) < 0.5f || player.Is_Walking == 1)
        { player.State_Switch(player.State_Walking); }
        

        //Checks for a state change
        if (player.input == Vector2.zero) { player.State_Switch(player.State_Idle); }
        else 
        {
        //player Animator
        player.Player_Anim.SetFloat("X", Mathf.Round(player.input.normalized.x) + Mathf.Round(Mathf.Abs(player.input.normalized.y) *-1 * Mathf.Round(player.input.normalized.x)));
        player.Player_Anim.SetFloat("Y", Mathf.Round(player.input.normalized.y));

        //Sets Item is hand floats if animator is present
        if(player.Item_in_Hand != null){
        //Gets Animator
        Animator Item_in_Hand_animator = player.Item_in_Hand.GetComponent<Animator>();

        if(Item_in_Hand_animator != null)
        {
            //Sets Floats
            Item_in_Hand_animator.SetFloat("X",player.Player_Anim.GetFloat("X"));
            Item_in_Hand_animator.SetFloat("Y",player.Player_Anim.GetFloat("Y"));

        }
        }

            //Last input dir
            Player_State_Machine.Last_Input_Dir = player.input;
            



        }
        

    }
    //Item one use
    public override void on_item_one_use(Player_State_Machine player)
    {

        

    }
    
}
