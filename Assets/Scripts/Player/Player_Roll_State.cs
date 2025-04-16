using System.Collections;
using UnityEngine;

public class Player_Roll_State : Player_Base_State
{
    public override void On_State_Enter(Player_State_Machine player)
    {
        Player_State_Machine.Last_Input_Dir = player.input;
        player.StartCoroutine(enumerator(player));
    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
        
    }

    public override IEnumerator enumerator(Player_State_Machine player)
    {
        float a = 150;
        while (a != 0)
        {
            player.Player_RB.linearVelocityY = ((Player_State_Machine.Last_Input_Dir.y + (player.input.y * 0.4f) - player._Angle.x * player.input.x) + (player._Angle.y * (player.input.y + player.input.y * 0.3f) * 0.4f)) * Time.fixedDeltaTime * player.Speed;
            player.Player_RB.linearVelocityX = Player_State_Machine.Last_Input_Dir.x + player.input.x * 0.4f * Time.fixedDeltaTime * player.Speed;
            //TODO: Make is so it ends when the rolling animation ends
            a--;
            yield return null;
            
        }

        player.State_Switch(player.State_Idle);

    }
        //Item one use
    public override void on_item_one_use(Player_State_Machine player)
    {

        

    }

    
}
