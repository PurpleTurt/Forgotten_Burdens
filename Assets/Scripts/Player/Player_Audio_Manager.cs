using UnityEngine;

public class Player_Audio_Manager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioSources;
    [SerializeField]
    private GameObject[] Effects;


    public void Step_Audio()
    {
        Instantiate(Effects[0]).transform.position = transform.position;
        audioSources[0].pitch = Random.Range(0.8f,1.2f);
        audioSources[0].Play();
    }
    public void Audio_1()
    {
        audioSources[1].Play();
    }
    public void Audio_2()
    {
        audioSources[2].Play();
    }
    public void Audio_3()
    {
        audioSources[3].Play();
    }
    public void Audio_4()
    {
        audioSources[4].Play();
    }
}
