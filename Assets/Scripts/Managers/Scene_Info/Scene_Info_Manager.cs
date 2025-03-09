using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;

public class Scene_Info_Manager : MonoBehaviour, IDay_Cycle_Effected
{
    [SerializeField]
    private Scene_Info Scene_info_Object;
    [SerializeField]
    private Shadow_Scriptable_Object Shadow_Data;
    [SerializeField]
    private Light2D Global_Light;
    [SerializeField]
    public AudioSource Ambiance_Source;

    private bool is_DayTime;

    


    //List of Objects with the IDay_Cycle_Effected Interface
    [SerializeField]
    private List<IDay_Cycle_Effected> Time_Effected_Objects()
    {
        List<IDay_Cycle_Effected> interfaces = new List<IDay_Cycle_Effected>();
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            IDay_Cycle_Effected[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IDay_Cycle_Effected>();
            foreach (var childInterface in childrenInterfaces)
            {
                interfaces.Add(childInterface);
            }
        }
        return interfaces;
    }


    [Range(0,24)]
    public float Scene_Time;
    public float Time_Speed;
    public bool Scene_Has_Frozen_Time = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //Loads Audio into Memory
        Scene_info_Object.Ambiance_Clips[0].LoadAudioData();
        Scene_info_Object.Ambiance_Clips[1].LoadAudioData();
        //Sets Lighting
        Global_Light.color = Scene_info_Object.Lighting.Evaluate(Scene_Time / 24);
        //Sets Ambience Audio Source Volume to 0 for smoother lerping when entering the scene
        Ambiance_Source.volume = 0;

        //Gets list of objects with the IDay_Cycle_Effected Interface
        Time_Effected_Objects();
        //Checks what the is day bool should be set to
        if (Scene_Time > 6 && Scene_Time < 18 && is_DayTime == false)
        {
            is_DayTime = true;
            
        }
        if (Scene_Time < 6 && is_DayTime == true || Scene_Time > 18 && is_DayTime == true)
        {
            is_DayTime = false;
            

        }
        //Runs the Routine in interface objects Depending on the time of day
        Run_Time_Objects();


       


    }

    // Update is called once per frame
    void Update()
    {
        //Sets the Time
        if (Scene_Has_Frozen_Time == false)
        {
            Scene_Time += Time_Speed * Time.fixedDeltaTime * 0.005f;
            

            Scene_Time %= 24;
            //Sets Lighting
            Global_Light.color = Scene_info_Object.Lighting.Evaluate(Scene_Time / 24);

            //Checks if it should update time relient objects
            if (Scene_Time > 6 && Scene_Time < 18 && is_DayTime == false)
            {
                is_DayTime = true;
                Run_Time_Objects();
            }
            if (Scene_Time < 6 && is_DayTime == true || Scene_Time > 18 && is_DayTime == true)
            {
                is_DayTime = false;
                Run_Time_Objects();
                
            }



        }
    }

    //Interface Stuff
    public void on_Daytime()
    {
        StartCoroutine(Ambience_Switch(0));
    }

    public void on_Nightime()
    {
        StartCoroutine(Ambience_Switch(1));

    }
    //Switched ambience at a smooth speed
    private IEnumerator Ambience_Switch(int Ambience_To_Switch_To)
    {

        while(Ambiance_Source.volume != 0)
        {
            
            Ambiance_Source.volume = Mathf.MoveTowards(Ambiance_Source.volume, 0, 0.01f);
            yield return new WaitForSecondsRealtime(0.1f);
            yield return null;
        }
        Ambiance_Source.clip = Scene_info_Object.Ambiance_Clips[Ambience_To_Switch_To];
        Ambiance_Source.Play();
        while (Ambiance_Source.volume != 0.3f)
        {
            
            Ambiance_Source.volume = Mathf.MoveTowards(Ambiance_Source.volume, 0.3f, 0.01f);
            yield return new WaitForSecondsRealtime(0.1f);
            yield return null;
        }
    }



    private void Run_Time_Objects()
    {
        foreach (IDay_Cycle_Effected Time_Cycle_Script in Time_Effected_Objects())
        {
            if (Scene_Time > 6 && Scene_Time < 18)
            {
                
                Time_Cycle_Script.on_Daytime();
            }
            else
            {
                
                Time_Cycle_Script.on_Nightime();
            }

        }
    }



#if UNITY_EDITOR
    private void OnValidate()
    {
        Global_Light.color = Scene_info_Object.Lighting.Evaluate(Scene_Time / 24);
    }
#endif


}
