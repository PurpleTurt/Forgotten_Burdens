using UnityEngine;

public class Camera_State_Machine : MonoBehaviour
{
    //States
    private Camera_Base_State Current_State;
    public Camera_Reposition_State State_Moving = new Camera_Reposition_State();
    public Camera_Idle_State State_Idle = new Camera_Idle_State();




    public GameObject Player;
    public float Speed;

    private void Start()
    {
        Player = FindFirstObjectByType<Player_State_Machine>().gameObject;

        //State Definition
        Current_State = State_Moving;
        Current_State.State_Enter(this);
    }
    void Update()
    {
        Current_State.State_Update(this);
    }

    public void Switch_State(Camera_Base_State State)
    {
        Current_State.State_Exit(this);
        Current_State = State;
        Current_State.State_Enter(this);
    }

}
