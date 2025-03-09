using System.Collections;
using UnityEngine;

public abstract class Player_Base_State
{

    public abstract void On_State_Enter(Player_State_Machine player);
    public abstract void State_Update(Player_State_Machine player);
    public abstract void On_State_Exit(Player_State_Machine player);
    public abstract IEnumerator enumerator(Player_State_Machine player);






}
