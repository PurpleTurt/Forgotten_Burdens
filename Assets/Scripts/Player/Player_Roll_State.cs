using System.Collections;
using UnityEngine;

public class Player_Roll_State : Player_Base_State
{
    

    public override void On_State_Enter(Player_State_Machine player)
    {
        player.Player_Anim.Play("Roll",0,0);
        player.Can_Roll = false;
        
        
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
        Vector2 DirToMove = (Player_State_Machine.Last_Input_Dir + player.input * 0.5f);
        
        player.Player_RB.linearVelocityY = ((DirToMove.y - player._Angle.x * DirToMove.x) + (player._Angle.y * DirToMove.y * 0.4f)) * Time.fixedDeltaTime * player.Speed;
        player.Player_RB.linearVelocityX = DirToMove.x * Time.fixedDeltaTime * player.Speed;



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
