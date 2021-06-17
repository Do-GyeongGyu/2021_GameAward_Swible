using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private string SaveFilepath;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        SaveFilepath = Application.persistentDataPath + "/savedata.save";
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void OnSave()
    {
        SaveData sd = CreateSaveDate();

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(SaveFilepath);

            bf.Serialize(file, sd);
            if (file != null)
                file.Close();

        Debug.Log(SaveFilepath);

        SceneManager.LoadScene("Title");
        
    }

    private SaveData CreateSaveDate()
    {
        SaveData sd = new SaveData();
        sd.stage = SceneManager.GetActiveScene().name;
        sd.playerpos_x = Player.transform.position.x;
        sd.playerpos_y = Player.transform.position.y;
        sd.playerpos_z = Player.transform.position.z;

        return sd;
    }
}
