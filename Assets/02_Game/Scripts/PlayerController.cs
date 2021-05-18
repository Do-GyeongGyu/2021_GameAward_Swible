using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    /* 操作方法
    
        移動      - 十字キー左右
        ジャンプ  - SPACE
    */

    // ステート
    public enum CharactorState
    {
        STATE_NORMAL, // 通常状態
        STATE_WARP,   // ワープ状態
    };

    // パラメータ
    public float _Speed         = 0.0f;                             // キャラクターの移動速度
    public float _MaxSpeed      = 5.0f;
    public float _JumpPower     = 10.0f;                            // ジャンプ力
    public float _Rate          = 0.1f;
    public float _Gravity       = 20.0f;                            // 重力の大きさ
    public Vector3 _DefaultPos  = new Vector3(0.0f, 0.0f, 0.0f);    // プレイヤーの初期位置
    public bool _IsFront        = true;                             // 表ステージかどうか

    private Ray _Ray;                                               // 当たり判定取得用のレイ
    private GameObject _RayCastHitObject;                           // レイが当たったオブジェクトを取得するための変数
    [SerializeField]private float _RayDistance = 0.0f;              // レイの長さ

    private CharacterController _Controller;        // コンポーネントの取得
    private Vector3 _MoveDirection = Vector3.zero;  // キャラクターの移動量
    private float _H;                               // キー入力取得用
    private CharactorState _CharactorState;         // キャラクターステート
    private GameObject _WorldMgr;                   // WorldMgrの情報格納用
    private GameObject _AerialMgr;                  // AerialMgrの情報格納用
    private GameObject _AerialCollision;            // プレイヤーと触れている足場格納用
    private GameObject _NearestAerial;              // プレイヤーに最も近い足場格納用
    private GameObject _CurrentNearestAerial;       // 現在プレイヤーに最も近い足場格納用

    private Animator _Animator;  // アニメーション遷移管理

    [SerializeField] private bool _PlayerDirectionRight; // プレイヤーの向いてる方向が右

    public Animator FadeMove;
    public float FadeMovingTime = 1f;

    // メンバ関数
    //************************************************
    //  キャラクターのステートセットする関数
    //  引数には列挙型を入れる
    //
    //  public enum CharactorState
    //  {
    //      STATE_NORMAL, // 通常状態
    //      STATE_WARP,   // ワープ状態
    //  };
    //************************************************
    public void SetState(CharactorState s)
    {
        // 現在のステートを変更
        this._CharactorState = s;

        // 各状態ごとの初期処理
        if(s == CharactorState.STATE_NORMAL)// 通常状態
        {
            _Controller.enabled = true;      // CharactorController有効化
        }
        else if(s == CharactorState.STATE_WARP)// ワープ状態
        {
            _Controller.enabled = false;        // CharactorController無効化
            _MoveDirection      = Vector3.zero; // 移動量をゼロにする
        }
    }

    //************************************************
    //  表ステージかどうかの変数を変える関数
    //  引数にはbool型を入れる
    //************************************************
    public void SetIsFront(bool f)
    {
        _IsFront = f;
    }

    //************************************************
    //  操作できる足場のアイコンを表示させる関数
    //************************************************
    public bool CheckNearestAerial()
    {
        if (_CurrentNearestAerial != _NearestAerial)// 現在の最も近い足場と取得した最も近い足場が異なる場合
            return true;
        else
            return false;
    }

    //************************************************
    //  Rayで取得したオブジェクトを返すGetter
    //************************************************
    public GameObject GetRayCastObject()
    {
        return _RayCastHitObject;
    }

    //************************************************
    //  プレイヤーの向きを返すGetter
    //************************************************
    public bool GetPlayerDirection()
    {
        return _PlayerDirectionRight;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Aerial")
        {
            _AerialCollision = hit.gameObject;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        // CharactorControllerのコンポーネントを取得
        _Controller = GetComponent<CharacterController>();

        // CharactorStateの初期化
        _CharactorState = CharactorState.STATE_NORMAL;   // 初期値を表に設定

        // WorldMgrの取得
        _WorldMgr = GameObject.Find("WorldMgr");

        // WorldMgrの取得
        _AerialMgr = GameObject.Find("AerialMgr");

        // _AerialCollisionの初期化
        _AerialCollision = null;

        // _NearestAerialの初期化
        _NearestAerial = null;

        // _CurrentNearestAerialの初期化
        _CurrentNearestAerial = null;

        // _Animatorを取得
        _Animator = GetComponent<Animator>();

        // プレイヤーの向き初期化
        _PlayerDirectionRight = true;

        if(LoadManager.IsFirst == false)
        {
            Debug.Log("来たんゴ");
            //プレイヤー位置ロード
            transform.position = LoadManager.playerpos;
            LoadManager.IsFirst = true;
        }

    }//Start

    IEnumerator WorldFade()
    {
        //キャラコン一時停止

        //アニメーション再生
        FadeMove.SetTrigger("Start");

        //待機
        yield return new WaitForSeconds(FadeMovingTime);

        if (_WorldMgr.GetComponent<WorldMgr>().GetWorldState() == WorldMgr.WorldState.STATE_FRONT)
        {
            _WorldMgr.GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_BACK);
        }
        else
        {
            _WorldMgr.GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_FRONT);
        }
    }

    public void MoveWorld()
    {
        StartCoroutine(WorldFade());
    }

    // Update is called once per frame
    void Update()
    {
        if (_CharactorState == CharactorState.STATE_NORMAL)// 通常状態
        {
            // キー入力取得
            _H = Input.GetAxis("Horizontal");    // 値の範囲(-1.0f~1.0f)
                                                 //if (Input.GetKey(KeyCode.A))
                                                 //{
                                                 //    _Speed -= _Rate * Time.deltaTime;
                                                 //    if (Mathf.Abs(_Speed) > _MaxSpeed)
                                                 //        _Speed = -_MaxSpeed;
                                                 //}
                                                 //else if (Input.GetKey(KeyCode.D))
                                                 //{
                                                 //    _Speed += (_MaxSpeed / _Rate) * Time.deltaTime;
                                                 //    if (_Speed > _MaxSpeed)
                                                 //        _Speed = _MaxSpeed;
                                                 //}

            // Rayの更新
            _Ray = new Ray(this.transform.position, -this.transform.up);
            RaycastHit rayCastHit;

            if (Physics.Raycast(_Ray.origin, _Ray.direction, out rayCastHit, _RayDistance))
            {
                if (rayCastHit.collider.gameObject.tag == "Aerial")
                    _RayCastHitObject = rayCastHit.collider.gameObject;
            }
            else
            {
                _RayCastHitObject = null;
            }

            // プレイヤーに最も近い足場のオブジェクトを取得
            _NearestAerial = _AerialMgr.GetComponent<AerialMgr>().GetNearestAerial();

            // 最も近い足場が変わったかチェック
            if (CheckNearestAerial())
            {
                if (_NearestAerial != null)// 取得した最も近い足場がnullではない場合
                {
                    if (_CurrentNearestAerial != null)// 現在の最も近い足場nullではない場合
                    {
                        _CurrentNearestAerial.GetComponent<AerialController>().ChangeButtonIconEnabled();   // 現在の足場のアイコンを切り替える（非表示）
                        _NearestAerial.GetComponent<AerialController>().ChangeButtonIconEnabled();          // 取得した足場のアイコンを切り替える（表示）

                        _CurrentNearestAerial = _NearestAerial; // 取得した足場を現在の足場に代入
                    }
                    else
                    {
                        _NearestAerial.GetComponent<AerialController>().ChangeButtonIconEnabled();  // 取得した足場のアイコンを切り替える（表示）

                        _CurrentNearestAerial = _NearestAerial; // 取得した足場を現在の足場に代入
                    }
                }
                else
                {
                    _CurrentNearestAerial.GetComponent<AerialController>().ChangeButtonIconEnabled();   // 現在の足場のアイコンを切り替える（非表示）

                    _CurrentNearestAerial = _NearestAerial; // 取得した足場を現在の足場に代入
                }
            }

            // プレイヤーの向き変更
            if(_PlayerDirectionRight)
            {
                if(_H < 0.0f)
                {
                    this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                    _PlayerDirectionRight = false;
                }
            }
            else
            {
                if (_H > 0.0f)
                {
                    this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    _PlayerDirectionRight = true;
                }
            }


            // キャラクターの移動
            if (_Controller.isGrounded)// キャラクターが地面についているとき
            {
                _MoveDirection = new Vector3(_H, 0.0f, 0.0f);                   // キー入力でx成分のみ移動量に加える
                _MoveDirection *= _Speed;                                       // キャラクターの設定スピードを乗算

                // アニメーション（歩き）
                if (Input.GetAxis("Horizontal") != 0.0f)
                    _Animator.SetBool("Speed", true);
                else
                    _Animator.SetBool("Speed", false);

                // アニメーション（着地）
                _Animator.SetBool("Jump", false);

                // ジャンプ
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))// SPACEキーが押されたとき
                {
                    _MoveDirection.y = _JumpPower;  // y成分にキャラクターのジャンプ力を加算
                }

                // 表と裏の変更
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton5))
                {
                    MoveWorld();

                    //if (_WorldMgr.GetComponent<WorldMgr>().GetWorldState() == WorldMgr.WorldState.STATE_FRONT)
                    //{
                    //    _WorldMgr.GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_BACK);
                    //}
                    //else
                    //{
                    //    _WorldMgr.GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_FRONT);
                    //}
                }

                // 足場の変更
                if(Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    // 取得したオブジェクトのステート（表裏）を切り替える
                    if (_NearestAerial != null)
                        _NearestAerial.GetComponent<AerialController>().ChangeState();
                }
            }
            else
            {
                _MoveDirection.x = _H;                                          // キー入力でx成分のみ移動量に加える
                _MoveDirection.x *= _Speed;                                     // キャラクターの設定スピードを乗算

                // アニメーション（ジャンプ、滞空）
                _Animator.SetBool("Jump", true);
                
                // _AerialCollisionを空にする
                _AerialCollision = null;

                //Debug.Log("a");

                //else if (!_Controller.isGrounded)
            }
            
            // 重力設定
            _MoveDirection.y -= _Gravity * Time.deltaTime;
            _Controller.Move(_MoveDirection * Time.deltaTime);
        }
        else if(_CharactorState == CharactorState.STATE_WARP)    // ワープ状態
        {
            transform.position = _DefaultPos;       // 初期座標に移動
            SetState(CharactorState.STATE_NORMAL);  // 通常状態に移行
        }

        
        

    }//Update

}
