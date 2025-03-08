using UnityEngine;

public abstract class Camera_Base_State
{
    public abstract void State_Enter(Camera_State_Machine Camer);
    public abstract void State_Exit(Camera_State_Machine Camer);
    public abstract void State_Update(Camera_State_Machine Camer);




}
