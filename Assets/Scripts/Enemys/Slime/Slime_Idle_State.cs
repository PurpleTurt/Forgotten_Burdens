using System.IO;
using UnityEngine;

public class Slime_Idle_State : Slime_Base_State
{


    public override void On_State_Enter(Slime_Enemy_Manager slime) { Debug.Log("Shlime"); }
    public override void On_State_Exit(Slime_Enemy_Manager slime) { }
    public override void State_Update(Slime_Enemy_Manager slime)
    {
        slime.Move_With_Path();
        

    }

}
