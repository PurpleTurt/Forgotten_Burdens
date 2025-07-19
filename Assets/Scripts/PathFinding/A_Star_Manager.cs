using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class A_Star_Manager : MonoBehaviour
{

    public static A_Star_Manager Instance;
    void Awake()
    {
        Instance = this;
    }

    public List<Node> GeneratePath(Node start, Node end)
    {
        return null;
    }
}
