using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public Animator FadeMove;
    private float MovingTime = 5f;
    private GameObject MainCam;
    private GameObject ClearText;
    private bool Clear = false;

    void Start()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");
        MainCam.GetComponent<CameraSetting>().enabled = true;
        ClearText = GameObject.FindGameObjectWithTag("ClearText");
        ClearText.SetActive(false);
    }

  

    void OnTriggerEnter(Collider other)
    {
        if(Clear==false)
        {
            if (other.gameObject.tag == "Player")
            {
                MainCam.GetComponent<CameraSetting>().enabled = false;

                MainCam.transform.position = MainCam.transform.position + new Vector3(0.0f, 0.0f, 7.0f);

                ClearText.SetActive(true);

                FadeWithLoadScene();
            }
            Clear = true;
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
