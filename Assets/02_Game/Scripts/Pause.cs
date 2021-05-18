using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public int Timeflag = 0;
    [SerializeField] private GameObject pausePanel;


    void Start()
    {
        pausePanel.SetActive(false);
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

    }
    private void resume()
    {
        Time.timeScale = 1;  // 再開
        pausePanel.SetActive(false);
    }
    void Update()
    {
        switch (Timeflag)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    pause();
                    Timeflag = 1;
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    resume();
                    Timeflag = 0;
                }
                break;
        }
        
    }

    public void Click()
    {
        resume();
        Timeflag = 0;
    }
}
