using System.Collections;
using UnityEngine;

public class Player_Roll_State : Player_Base_State
{
    

    public override void On_State_Enter(Player_State_Machine player)
    {
        player.Player_Anim.Play("Player_Roll_Down",0,0);
        player.Can_Roll = false;
        
        
        
    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
        
        
        player.Player_RB.linearVelocityY = (Player_State_Machine.Last_Input_Dir.y + player.input.y * 0.5f) * Time.fixedDeltaTime * player.Speed;
        player.Player_RB.linearVelocityX = (Player_State_Machine.Last_Input_Dir.x + player.input.x * 0.5f) * Time.fixedDeltaTime * player.Speed;



    }
    private void Update()
    {

    }

    public override IEnumerator enumerator(Player_State_Machine player)
    {
        
        throw new System.NotImplementedException();

    }
        //Item one use
    public override void on_item_one_use(Player_State_Machine player)
    {

        

    }

    
}
