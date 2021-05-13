using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFadeMgr : MonoBehaviour
{
    public Animator FadeMove;
    public float MovingTime = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            WorldFadeOn();
        }
    }


    IEnumerator FadeMoving()
    {
        //アニメーション再生
        FadeMove.SetTrigger("Start");

        //待機
        yield return new WaitForSeconds(MovingTime);

    }

    public void WorldFadeOn()
    {
        StartCoroutine(FadeMoving());
    }
}
