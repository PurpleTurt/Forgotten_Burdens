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
        if(Mathf.Abs(player.input.y) < 0.5f && Mathf.Abs(player.input.x) > 0.1f)
        {
            player.Player_Anim.SetFloat("X", player.input.x);
            player.Player_Anim.SetFloat("Y", 0);
            
        }
        else {
            player.Player_Anim.SetFloat("Y", player.input.y);
            player.Player_Anim.SetFloat("X", 0);
            
        }
        

        

        

        
        

        //Flips The Object
        if(player.Player_Anim.GetFloat("X") <= 0.5f && player.Is_Left == false || player.Player_Anim.GetFloat("X") >= 0.5f && player.Is_Left == true)
        {
            player.Flip();
        }
        if(player.input == Vector2.zero) { player.State_Switch(player.State_Idle); }


    }
}
