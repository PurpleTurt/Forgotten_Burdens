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
        //Sets Player Animation Floats
        player.Player_Anim.SetFloat("X", Mathf.Round(Player_State_Machine.Last_Input_Dir.normalized.x) + Mathf.Round(Mathf.Abs(Player_State_Machine.Last_Input_Dir.normalized.y) * -1 * Mathf.Round(Player_State_Machine.Last_Input_Dir.normalized.x)));
        player.Player_Anim.SetFloat("Y", Mathf.Round(Player_State_Machine.Last_Input_Dir.normalized.y));

        //Sets Item is hand floats if animator is present
        if(player.Item_in_Hand != null){
        //Gets Items Animator
        Animator Item_in_Hand_animator = player.Item_in_Hand.GetComponent<Animator>();

        if(Item_in_Hand_animator != null)
        {
            //Sets Floats and Plays Animation
            Item_in_Hand_animator.Play("Idle",0,0);
            Item_in_Hand_animator.SetFloat("X",player.Player_Anim.GetFloat("X"));
            Item_in_Hand_animator.SetFloat("Y",player.Player_Anim.GetFloat("Y"));

        }
        }
        player.Player_RB.linearVelocity = Vector2.zero;
        
        player.Can_Roll = false;
        //Plays Player Idle Animation
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
