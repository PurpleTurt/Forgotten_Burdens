using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Switch_Test : MonoBehaviour
{
    [SerializeField]
    private int scene_To_Switch_To;
    public void on_Activation()
    {
        Save_Manager.instance.Save_Game();

        SceneManager.LoadScene(scene_To_Switch_To);
        
    }
}
