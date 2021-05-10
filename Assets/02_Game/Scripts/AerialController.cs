using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialController : MonoBehaviour
{
    // ステート
    public enum AerialState
    {
        STATE_FRONT,
        STATE_BACK,
    };

    // パラメータ
    [SerializeField] private AerialState _AerialState = AerialState.STATE_FRONT;    // 現在のステート格納用、初期値は表
    private GameObject _WorldMgr;                                                   // WorldMgrのオブジェクト情報を持っておく

    // メンバ関数
    //************************************************
    //  現在のステートを入れ替える関数
    //  関数を呼ぶ度に入れ替わる
    //************************************************
    public void ChangeState()
    {
        if (_AerialState == AerialState.STATE_FRONT)
            _AerialState = AerialState.STATE_BACK;
        else
            _AerialState = AerialState.STATE_FRONT;
    }

    //************************************************
    //  現在のステートを渡すGetter関数
    //************************************************
    public AerialState GetAerialState()
    {
        return _AerialState;
    }

    // Start is called before the first frame update
    void Start()
    {
        _WorldMgr = GameObject.Find("WorldMgr");    // WorldMgrを探して保持
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
