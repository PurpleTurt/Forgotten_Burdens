using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Save_Manager : MonoBehaviour
{
    //Game_Data
    private Game_Data gameData;
    [SerializeField]
    private string File_Name;
    private File_Data_Manager file_data;

    public static Save_Manager instance {  get; private set; }
    //Sets Up the Singleton
    private void Awake()
    {
        if(instance != null) 
        { 
            Destroy(gameObject);
            return;
        }
        else { instance = this; DontDestroyOnLoad(gameObject.transform.root); }
        file_data = new File_Data_Manager();
            
    }


    public void New_Game() { gameData = new Game_Data(); }

    public void Load_Game() 
    {
        //Checks if data exists
        if (gameData == null)
        {
            Debug.Log("No Game Data Found");
            New_Game();
        }
        else
        {

            //Load Data into Every Object
            foreach (ISave_Data Save_Data_Obj in Find_Save_Objects())
            {
                Save_Data_Obj.load(gameData);
            }
        }
    }

    public void Save_Game() 
    {
        foreach (ISave_Data Save_Data_Obj in Find_Save_Objects())
        {
            Save_Data_Obj.Save(ref gameData);
        }

    }

    //Gets Every Object With the Save Data Interface
    private List<ISave_Data> Find_Save_Objects() 
    { 
        IEnumerable<ISave_Data> Save_Objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISave_Data>();
        return new List<ISave_Data>(Save_Objects);
    
    }

    //Scene transition Management
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    public void OnSceneUnloaded(Scene scene)
    {
        
    }



    // Writes/Loads Data from File
    public void Save_to_file()
    {
        Save_Game();
        file_data.Save_Data_To_file(gameData);
    }
    public void Load_From_File()
    {

    }
}
