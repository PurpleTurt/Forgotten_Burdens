using UnityEngine;

public class Player_Walking_State : Player_Base_State
{
    public override void On_State_Enter(Player_State_Machine player)
    {
        player.Player_Anim.Play("Walk",0,0);
    }

    public override void On_State_Exit(Player_State_Machine player)
    {
        
    }

    public override void State_Update(Player_State_Machine player)
    {
        //Movement
        player.Player_RB.linearVelocityY = ((player.input.y - player._Angle.x * player.input.x) + (player._Angle.y * player.input.y * 0.4f)) * Time.deltaTime * player.Speed;
        player.Player_RB.linearVelocityX = (player.input.x) * Time.deltaTime * player.Speed;
        //Animator
        player.Player_Anim.SetFloat("Y", player.input.y);
        player.Player_Anim.SetFloat("X", player.input.x);
        

        

        
        
        //State Switch
        if (player.input == new Vector2(0,0))
        {
            player.State_Switch(player.State_Idle);
        }
        //Flips The Object
        if(player.input == new Vector2(-1,0) && player.Is_Left == false || player.input != new Vector2(-1, 0) && player.Is_Left == true)
        {
            player.Flip();
        }


    }
}
