using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public int Timeflag = 0;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pausePanel2;

    void Start()
    {
        pausePanel.SetActive(false);
        pausePanel2.SetActive(false);

    }

    //private void Pause()
    //{
    //    Time.timeScale = 0;  // 時間停止
    //    pausePanel.SetActive(true);
    //}
    private void pause()
    {
        Time.timeScale = 0;  // 時間停止
        pausePanel.SetActive(true);
        pausePanel2.SetActive(true);

    }
    private void resume()
    {
        Time.timeScale = 1;  // 再開
        pausePanel.SetActive(false);
        pausePanel2.SetActive(false);

    }
    void Update()
    {
        switch (Timeflag)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
                {
                    pause();
                    Timeflag = 1;
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
                {
                    resume();
                    Timeflag = 0;
                }
                break;
        }
        
    }
}
