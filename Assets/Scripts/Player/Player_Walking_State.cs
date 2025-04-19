using System.Collections;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Player_Walking_State : Player_Base_State
{
    
    //Spin Attack Check
    public override IEnumerator enumerator(Player_State_Machine player)
    {

        throw new System.NotImplementedException();
    }

    public override void On_State_Enter(Player_State_Machine player)
    {
        
        player.Player_Anim.Play("Walk", 0, 0);
        player.Can_Roll = true;
        
    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
        //Movement

            player.Player_RB.linearVelocityY = ((player.input.normalized.y - player._Angle.normalized.x * player.input.normalized.x + (player._Angle.normalized.y * player.input.normalized.y * 0.4f)) * Time.fixedDeltaTime * player.Speed) * 0.3f;
            player.Player_RB.linearVelocityX = (player.input.normalized.x) * player.Speed * 0.3f * Time.fixedDeltaTime;


        //Checks for a state change
        if (Mathf.Abs(player.input.x) > 0.6f && player.Is_Walking == 0 || Mathf.Abs(player.input.y) > 0.6f && player.Is_Walking == 0) 
        { player.State_Switch(player.State_Running); }

        if (player.input == Vector2.zero) { player.State_Switch(player.State_Idle); }
        else
        {
            player.Player_Anim.SetFloat("X", Mathf.Round(player.input.normalized.x) + Mathf.Round(Mathf.Abs(player.input.normalized.y) * -1 * Mathf.Round(player.input.normalized.x)));
            player.Player_Anim.SetFloat("Y", Mathf.Round(player.input.normalized.y));

            //Last input dir
            Player_State_Machine.Last_Input_Dir = player.input;
            

        }

    }
        //Item one use
    public override void on_item_one_use(Player_State_Machine player)
    {

    }
}
