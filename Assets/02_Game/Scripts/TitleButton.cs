using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public static bool IsFirst = true;
    private string SaveFilepath;
    public static Vector3 playerpos;
    private string Stage;

    // Start is called before the first frame update
    void Start()
    {
        SaveFilepath = Application.persistentDataPath + "/savedata.save";
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Erea 1-1 tutorial");
    }

    public void OnLoadButtonClicked()
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

    public void OnExitButtonClicked()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

    #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();

    #endif
    }
}
