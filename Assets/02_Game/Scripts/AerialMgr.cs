using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialMgr : MonoBehaviour
{
    // パラメータ
    [SerializeField] private float _AerialSearchDistance    = 10.0f;                        // プレイヤーが足場にインタラクトできる距離
    private List<GameObject> _AerialList                    = new List<GameObject>();       // 全ての足場を管理するリスト
    private GameObject _WorldMgr;                                                           // WorldMgr取得用
    private GameObject _Player;                                                             // プレイヤーのオブジェクト情報格納用

    // メンバ関数
    public GameObject GetNearestAerial()
    {
        // 変数
        float minDistance           = float.MaxValue;   // 最も短い距離を格納する変数、float型の最大値で初期化
        GameObject nearestAerial    = null;             // プレイヤーに最も近い足場オブジェクトを返すための変数、nullで初期化

        // プレイヤーに最も近い足場を返す
        foreach(var target in _AerialList)
        {
            float targetDistance = Vector3.Distance(_Player.transform.position, target.transform.position); // プレイヤーの足場の距離を算出
            bool targetInSight = false;                                                                     // プレイヤーの視界に入っているか

            if (_Player.GetComponent<PlayerController>().GetPlayerDirection())
            {
                if (_Player.transform.position.x < target.transform.position.x)
                    targetInSight = true;
            }
            else
            {
                if (_Player.transform.position.x > target.transform.position.x)
                    targetInSight = true;
            }


            //**********************************************************
            //  以下の条件を全て満たしているとき
            //  1,プレイヤーと足場の距離が最も短い場合
            //  2,プレイヤーと足場の距離が既定の距離よりも短い場合
            //  3,足場のステートとステージのステートが一致してる場合
            //  4,足場にプレイヤーがのっていない場合
            //  5,プレイヤーの視界に入っている場合
            //**********************************************************
            if (targetDistance < minDistance && 
                targetDistance < _AerialSearchDistance && 
                (int)target.GetComponent<AerialController>().GetAerialState() == (int)_WorldMgr.GetComponent<WorldMgr>().GetWorldState() &&
                _Player.GetComponent<PlayerController>().GetRayCastObject() != target.gameObject &&
                targetInSight)
            {
                minDistance     = targetDistance;               // プレイヤーに最も短い距離を代入
                nearestAerial   = target.transform.gameObject;  // プレイヤーに最も近い足場オブジェクトを返却用変数に格納
            }
        }

        // 返却
        return nearestAerial;
    }



    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーのオブジェクトを取得
        _Player = GameObject.Find("Player");

        // 全ての足場のオブジェクト情報をリストに格納
        foreach(Transform child in this.transform)
        {
            _AerialList.Add(child.gameObject);
        }

        // WorldMgrを取得
        _WorldMgr = GameObject.Find("WorldMgr");

    }//Start

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject target in _AerialList)
        {
            // 足場の表示、非表示
            if (_WorldMgr.GetComponent<WorldMgr>().GetWorldState() == WorldMgr.WorldState.STATE_FRONT)// WorldMgrのステートが表だった場合
            {
                if (target.GetComponent<AerialController>().GetAerialState() == AerialController.AerialState.STATE_FRONT)// 自分のステートが表だった場合
                {
                    target.gameObject.SetActive(true);    // 表示
                }
                else
                {
                    target.gameObject.SetActive(false);   // 非表示
                }
            }
            else
            {
                if (target.GetComponent<AerialController>().GetAerialState() == AerialController.AerialState.STATE_FRONT)
                {
                    target.gameObject.SetActive(false);
                }
                else
                {
                    target.gameObject.SetActive(true);
                }
            }
        }
    }//Update

}
