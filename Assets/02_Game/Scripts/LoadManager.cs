using System.IO;
//using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public static bool IsFirst = true;
    private string SaveFilepath;
    public static Vector3 playerpos;
    public string Stage;
    // [SerializeField] private GameObject pausePanel;

    void Start()
    {
        SaveFilepath = Application.persistentDataPath + "/savedata.save";
       // pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        if (File.Exists(SaveFilepath))
        {
            // バイナリ形式でデシリアライズ
            BinaryFormatter bf = new BinaryFormatter();
            // 指定したパスのファイルストリームを開く
            FileStream file = File.Open(SaveFilepath, FileMode.Open);

            // 指定したファイルストリームをオブジェクトにデシリアライズ。
            SaveData sd = (SaveData)bf.Deserialize(file);
            // 読み込んだデータを反映。
            playerpos.x = sd.playerpos_x;
            playerpos.y = sd.playerpos_y;
            playerpos.z = sd.playerpos_z;
            Stage = sd.stage;
            Debug.Log(playerpos);
            Debug.Log(Stage);

            // ファイル操作には明示的な破棄が必要です。Closeを忘れないように。
            if (file != null)
                file.Close();

            IsFirst = false;

            SceneManager.LoadScene(Stage);


        }
        else
        {
            Debug.Log("no load file");
        }
    }
}
