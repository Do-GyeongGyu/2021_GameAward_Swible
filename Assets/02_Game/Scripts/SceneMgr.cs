using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public Animator FadeMove;
    public float MovingTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            FadeWithLoadScene();
        }
    }

 
    IEnumerator LoadStage(int StageIndex)
    {
        //アニメーション再生
        FadeMove.SetTrigger("Start");

        //待機
        yield return new WaitForSeconds(MovingTime);

        //ステージロード
        SceneManager.LoadScene(StageIndex);
    }    

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            FadeWithLoadScene();
        }
    }

    public void FadeWithLoadScene()
    {
        StartCoroutine(LoadStage(SceneManager.GetActiveScene().buildIndex + 1));
    }

}
