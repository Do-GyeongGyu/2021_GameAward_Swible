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
    private GameObject _ButtonIcon;                                                 // ButtonIcon操作用変数
    private bool _ButtonIconEnabled = false;                                        // ButtonIconが表示されているか
    //[SerializeField] private GameObject effectPrefab;//エフェクトを入れる所


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
    //  アイコンの表示を切り替える関数
    //  関数を呼ぶ度に切り替わる
    //************************************************
    public void ChangeButtonIconEnabled()
    {
        if (_ButtonIconEnabled)
            _ButtonIconEnabled = false;
        else
            _ButtonIconEnabled = true;
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
        _WorldMgr = GameObject.Find("WorldMgr");                                // WorldMgrを探して保持
        _ButtonIcon = gameObject.transform.Find("ButtonIcon").gameObject;  // ButtonIconを取得
        _ButtonIcon.SetActive(false);                                           // 初期状態は非表示にしておく
        GetComponent<Outline>().enabled = false;
 //       GameObject effect = Instantiate(effectPrefab, transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        // ButtonIconの表示切り替え
        if (_ButtonIconEnabled)
        {
            _ButtonIcon.SetActive(true);
            GetComponent<Outline>().enabled = true;
        }
        else
        {
            if (_ButtonIcon != null)
            {
                _ButtonIcon.SetActive(false);
                GetComponent<Outline>().enabled = false;
            }
        }
         
    }
}
