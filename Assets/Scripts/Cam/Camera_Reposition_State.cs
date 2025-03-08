using UnityEngine;

public class Camera_Reposition_State : Camera_Base_State
{
    public override void State_Enter(Camera_State_Machine Camer)
    {
        
    }

    public override void State_Exit(Camera_State_Machine Camer)
    {
        
    }

    public override void State_Update(Camera_State_Machine Camer)
    {
        
        Vector3 vector2 = Camer.transform.position;
        
        if (Vector2.Distance(Camer.transform.position,Camer.Player.transform.position) > 0.05f)
        {
            vector2 = Vector3.MoveTowards(vector2, Camer.Player.transform.position, Camer.Speed * Time.deltaTime);
            vector2.z = -10;
            Camer.transform.position = vector2;
        }
    }
}
