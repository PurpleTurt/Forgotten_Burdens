using UnityEngine;

public class Player_Idle_State : Player_Base_State
{
    public override void On_State_Enter(Player_State_Machine player)
    {
        player.Player_Anim.Play("Idle", 0, 0);
    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
        if (player.input != new Vector2(0, 0))
        {
            player.State_Switch(player.State_Walking);
        }
    }
}
