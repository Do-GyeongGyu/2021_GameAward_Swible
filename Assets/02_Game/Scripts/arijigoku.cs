using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class arijigoku : MonoBehaviour
{

    public Transform objA;//プレイヤーを入れる
    public Transform objB;//蟻地獄のオブジェクト

    public float speed = 0.01F;//初期値（仮）

    private CharacterController _Controller;//キャラクターコントローラー
    private Vector3 distanceAB;//距離

    void Start()
    {
        //キャラクターコントローラー取得
        _Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //プレイヤーと蟻地獄の距離を求める
        distanceAB = objB.position - objA.position;
        //Z軸が動かないように設定
        distanceAB.z = 0;
        //距離を求め格納
        float length = (distanceAB).magnitude;
        //正規化
        distanceAB = (distanceAB).normalized;
        //蟻地獄の引き込み判定
        if (length <= objB.transform.localScale.x/ 2)
        {
            // 現在の位置
            //float present_Location = (Time.deltaTime * speed) / distanceAB;
            //Lerp処理
            // transform.position = Vector3.Lerp(objA.position, objB.position, present_Location);
            //キャラクターコントローラで座標が固定されてるためMoveでうごかす
            _Controller.Move(distanceAB * speed * Time.deltaTime);
            
        }
       
    }
}

