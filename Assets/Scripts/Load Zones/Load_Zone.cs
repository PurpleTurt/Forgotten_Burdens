using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Load_Zone : MonoBehaviour
{

    public int Load_Zone_ID;
    public int scene_To_Load_To;

    [SerializeField]
    private Vector2 Dir_To_Walk_Towards;


    private Animator transition;

    void Start()
    {
        //Gets Transition Gameobject
        transition = GameObject.Find("Transition").GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player_State_Machine player = collision.GetComponent<Player_State_Machine>();
        if(player != null)
        {
            //Sets player load id
            Player_State_Machine.Load_Zone_ID = Load_Zone_ID;
            StartCoroutine(Scene_Switch(player));

        }
    }
    
    private IEnumerator Scene_Switch(Player_State_Machine player)
    {
        //Locks player controls and plays transition animation
        player.Locked_Controls = true;
        transition.Play("Trans",0,0);
        UnityEngine.UI.Image image = transition.gameObject.GetComponent<UnityEngine.UI.Image>();
        //Gets Audio Sources for lerping
        AudioSource Ambience = GameObject.Find("Ambience_Source").GetComponent<AudioSource>();
        AudioSource music = GameObject.Find("Scene_Music").GetComponent<AudioSource>();
        while(image.color != Color.black)
        {
            //Sets Player input
            player.input = Dir_To_Walk_Towards * 0.5f;
            //Lerps Audio volume to 0
            music.volume = Mathf.Lerp(music.volume,0,Time.fixedDeltaTime);
            Ambience.volume = Mathf.Lerp(music.volume,0,Time.fixedDeltaTime);
            
            
            yield return null;
        }
        //Loads new scene
        SceneManager.LoadSceneAsync(scene_To_Load_To);
    }

}
