using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    [SerializeField] float _WaitSeconds = 0;    // 触れてから落ちるまでの秒数

    private bool _Falling;                      // 床が落ちているか
    private Vector3 _DefaultPos;                // 床の初期位置

    private GameObject _Player;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        _Falling = false;

        _DefaultPos = transform.position;

        _Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーを感知するレイを飛ばす
        RaycastHit hit;
        if (Physics.BoxCast(transform.position + new Vector3(0.2f, -1.0f, 0.0f), Vector3.one * 2.0f, transform.up, out hit, transform.rotation, 1.0f))
        {
            if (hit.transform.tag == "Player")
            {
                StartCoroutine(Fall(_WaitSeconds));
            }
        }

        if (_Falling)
        {
            transform.position += new Vector3(0.0f, -1.0f, 0.0f);
        }

        if (_Player.GetComponent<PlayerController>().GetPlayerState() == PlayerController.CharactorState.STATE_WARP)
        {
            StopAllCoroutines();
            transform.position = _DefaultPos;
            _Falling = false;
        }
    }

    private IEnumerator Fall(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        _Falling = true;    // 落下開始
    }

    // 衝突判定
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Death")// 衝突したオブジェクトが"Player"タグの着いたオブジェクトだった場合
        {
            transform.position += new Vector3(0.0f, -10.0f, 0.0f);
            _Falling = false;
        }
    }
}
