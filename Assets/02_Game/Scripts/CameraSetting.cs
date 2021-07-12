using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    [SerializeField] private GameObject _Player;                                        // プレイヤーのオブジェクト情報格納用
    [SerializeField] private Vector3 _OffsetPosition = new Vector3(0.0f, 0.0f, 0.0f);   // プレイヤーを中心としたオフセットポジション
    private GameObject _WorldMgr;

    private bool Follow;    // プレイヤーに追従するか

    public void FollowEnable(bool b)
    {
        Follow = b;
    }

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの情報を取得
        _Player = GameObject.Find("Player");

        // WorldMgrの取得
        _WorldMgr = GameObject.Find("WorldMgr");

        // グレースケールスクリプトの無効化
        GetComponent<MonoTonePostEffect>().enabled = false;

        // プレイヤーに追従
        Follow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Follow)
        {
            // プレイヤーに追従
            this.transform.position = _Player.transform.position + _OffsetPosition;
        }


        // モノトーン切り替え
        if(_WorldMgr.GetComponent<WorldMgr>().GetWorldState() == WorldMgr.WorldState.STATE_FRONT)
        {
            GetComponent<MonoTonePostEffect>().enabled = false;
        }
        else
        {
            GetComponent<MonoTonePostEffect>().enabled = true;
        }
    }
}
