using System.Collections;
using UnityEngine;

public class Player_Attacking_State : Player_Base_State
{
    public override void On_State_Enter(Player_State_Machine player)
    {
        player.Player_Anim.Play("Attack",0,0);
        //Makes Sure only one instance of the coroutine is active
        player.StopAllCoroutines();
        //Starts Coroutine
        player.StartCoroutine(enumerator(player));
        //Sets Velocity
        player.Player_RB.linearVelocity = Vector2.zero;
        player.Can_Roll = false;
        

    }
    public override void State_Update(Player_State_Machine player)
    {
            


            //Test if you can Roll Out
            if(player.input != Vector2.zero)
            {
                player.Can_Roll = true;
                //Last input dir
                Player_State_Machine.Last_Input_Dir = player.input;
            }

    }
    public override void On_State_Exit(Player_State_Machine player){player.StopAllCoroutines();}

    public override void on_item_one_use(Player_State_Machine player){}
    public override IEnumerator enumerator(Player_State_Machine player)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        //Gets Animation Clip Name
        string Current_Anim = player.Player_Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //Wait for clip to end to switch states
        yield return new WaitWhile(() => player.Player_Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == Current_Anim);
        
        player.State_Switch(player.State_Idle);
    }
}
