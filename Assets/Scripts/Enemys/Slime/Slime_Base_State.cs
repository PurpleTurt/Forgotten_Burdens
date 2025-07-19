using UnityEngine;

public abstract class Slime_Base_State
{


    public abstract void On_State_Enter(Slime_Enemy_Manager slime);
    public abstract void On_State_Exit(Slime_Enemy_Manager slime);
    public abstract void State_Update(Slime_Enemy_Manager slime);

}
