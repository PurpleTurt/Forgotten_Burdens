using System.IO;
using UnityEngine;

public class File_Data_Manager
{


    public void Save_Data_To_file(Game_Data data)
    {

        string Data_to_Store = JsonUtility.ToJson(data, true);
        using (FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath,"Save.json"), FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream)) 
            { 
                writer.Write(Data_to_Store);
            }

        }

    }

}
