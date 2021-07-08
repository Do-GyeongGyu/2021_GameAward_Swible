using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class taimer : MonoBehaviour
{
    private GameObject _Player;

    public Image Gage;
    public bool roop;
    public float countTime;

    void Start()
    {
        _Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if (roop)
            Gage.fillAmount -= 1.0f / countTime * Time.deltaTime;

        if(Gage.fillAmount <= 0)
        {
            _Player.GetComponent<PlayerController>().SetState(PlayerController.CharactorState.STATE_WARP);
            Gage.fillAmount = 1;
            GameObject.Find("WorldMgr").GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_FRONT);    // 表ステージに変更
        }
    }
}
