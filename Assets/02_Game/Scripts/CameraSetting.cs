using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    [SerializeField] private GameObject _Player;                                        // プレイヤーのオブジェクト情報格納用
    [SerializeField] private Vector3 _OffsetPosition = new Vector3(0.0f, 0.0f, 0.0f);   // プレイヤーを中心としたオフセットポジション

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの情報を取得
        _Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーに追従
        this.transform.position = _Player.transform.position + _OffsetPosition;
    }
}
