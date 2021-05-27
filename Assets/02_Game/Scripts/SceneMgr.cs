using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public Animator FadeMove;
    private float MovingTime = 5.0f;

    public string NextSceanName;


    void Start()
    {
        //MainCam = GameObject.FindGameObjectWithTag("MainCamera");
        //MainCam.GetComponent<CameraSetting>().enabled = true;
        //ClearText = transform.Find("ClearText").gameObject;
        //ClearText.SetActive(false);
        //Clear = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(NextSceanName);
    }     

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            SceneManager.LoadScene(NextSceanName);
        }

    }

    IEnumerator LoadStage(int StageIndex)
    {
        //フェードアニメーション再生
        FadeMove.SetTrigger("Start");

        //待機
        yield return new WaitForSeconds(MovingTime);

        //ステージロード
        SceneManager.LoadScene(StageIndex);
    }

    public void FadeWithLoadScene()
    {
        StartCoroutine(LoadStage(SceneManager.GetActiveScene().buildIndex + 1));
    }



}
