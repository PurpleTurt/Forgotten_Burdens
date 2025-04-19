using System.Collections;
using UnityEngine;

public class Player_Idle_State : Player_Base_State
{
    public override IEnumerator enumerator(Player_State_Machine player)
    {
        throw new System.NotImplementedException();
    }

    public override void On_State_Enter(Player_State_Machine player)
    {

        player.Player_Anim.SetFloat("X", Mathf.Round(Player_State_Machine.Last_Input_Dir.normalized.x) + Mathf.Round(Mathf.Abs(Player_State_Machine.Last_Input_Dir.normalized.y) * -1 * Mathf.Round(Player_State_Machine.Last_Input_Dir.normalized.x)));
        player.Player_Anim.SetFloat("Y", Mathf.Round(Player_State_Machine.Last_Input_Dir.normalized.y));
        player.Player_RB.linearVelocity = Vector2.zero;
        
        player.Can_Roll = false;

        player.Player_Anim.Play("idle", 0, 0);

    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
        if (player.input != new Vector2(0, 0))
        {
            //State Switching
            if (Mathf.Abs(player.input.x) < 0.6f && Mathf.Abs(player.input.y) < 0.6f || player.Is_Walking == 1)
            { player.State_Switch(player.State_Walking); }
            else { player.State_Switch(player.State_Running); }
        }

    }
    //Item one use
    public override void on_item_one_use(Player_State_Machine player)
    {

    }
}
