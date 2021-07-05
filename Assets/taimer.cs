using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class taimer : MonoBehaviour
{
    public Image Gage;
    //public bool roop;
    public float countTime;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (roop)
        //{
            Gage.fillAmount -= 1.0f / countTime * Time.deltaTime;
       // }

        //if(countTime>0)
        //{
        //    GameObject.Find("WorldMgr").GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_FRONT);    // 表ステージに変更
        //    col.GetComponent<PlayerController>().SetState(PlayerController.CharactorState.STATE_WARP);              // プレイヤーをワープ状態にする
        //}

    }
}
