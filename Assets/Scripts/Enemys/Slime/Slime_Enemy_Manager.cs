using System.Collections.Generic;
using UnityEngine;

public class Slime_Enemy_Manager : MonoBehaviour
{

    //States
    public Slime_Base_State current_state;
    public Slime_Idle_State Idle_State = new Slime_Idle_State();

    //Path Finding Nodes
    public Node currentNode;

    public Node[] nodes;
    public List<Node> Path = new List<Node>();

    //Stats
    public float speed;

    //Components
    public Rigidbody2D Slime_RB;

    void Start()
    {
        //Gets Components
        Slime_RB = GetComponent<Rigidbody2D>();


        //Enters State
        current_state = Idle_State;
        current_state.On_State_Enter(this);
    }

    void Update()
    {
        current_state.State_Update(this);
    }

    public void switch_state(Slime_Base_State New_State)
    {
        current_state.On_State_Exit(this);
        current_state = New_State;
        current_state.On_State_Enter(this);
    }


    //Pathfinding Path Generation
    public void Move_With_Path()
    {

        if (Path != null || Path.Count > 0)
        {

            int x = 0;
            //Moves slime towards position of the next node
            Slime_RB.position = Vector2.MoveTowards(Slime_RB.position, new Vector2(Path[x].transform.position.x, Path[x].transform.position.y), speed * Time.deltaTime);

            //Changes current set destination node if you get close enough and removes the old one
            if (Vector2.Distance(transform.position, Path[x].transform.position) <= 0.1f)
            {
                currentNode = Path[x];
                Path.RemoveAt(x);

            }
        }
        else
        {
            //Gets List of nodes in order to set a path using the A Star Manager in the scene
            nodes = FindObjectsByType<Node>(FindObjectsSortMode.InstanceID);

            Path = A_Star_Manager.Instance.GeneratePath(currentNode, nodes[2]);
                
            

        }

    }
}
