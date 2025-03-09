using System.Collections;
using UnityEngine;

public class Player_Walking_State : Player_Base_State
{
    public override IEnumerator enumerator(Player_State_Machine player)
    {
        throw new System.NotImplementedException();
    }

    public override void On_State_Enter(Player_State_Machine player)
    {
        
        player.Player_Anim.Play("idle", 0, 0);
    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
        //Movement
        if (Mathf.Abs(player.input.x) >= 0.1f || Mathf.Abs(player.input.y) >= 0.1f)
        {
            player.Player_RB.linearVelocityY = ((player.input.y - player._Angle.x * player.input.x + (player._Angle.y * player.input.y * 0.4f)) * Time.deltaTime * player.Speed) * 0.5f;
            player.Player_RB.linearVelocityX = (player.input.x) * Time.deltaTime * player.Speed * 0.5f;
        }
        else { player.Player_RB.linearVelocity = Vector2.zero; }

        //Checks for a state change
        if (Mathf.Abs(player.input.x) > 0.6f && player.Is_Walking == 0 || Mathf.Abs(player.input.y) > 0.6f && player.Is_Walking == 0) 
        { player.State_Switch(player.State_Running); }

        if (player.input == Vector2.zero) { player.State_Switch(player.State_Idle); }
        else
        {
            player.Player_Anim.SetFloat("X", Mathf.Round(player.input.normalized.x) + Mathf.Round(Mathf.Abs(player.input.normalized.y) * -1 * Mathf.Round(player.input.normalized.x)));
            player.Player_Anim.SetFloat("Y", Mathf.Round(player.input.normalized.y));


        }
    }
}
