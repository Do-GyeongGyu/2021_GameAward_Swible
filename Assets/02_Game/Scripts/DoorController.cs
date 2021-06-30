using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject _DoorLeft;   // ドア左
    private GameObject _DoorRight;  // ドア右

    [SerializeField] private float _DoorRotAngle = 90.0f;    // ドアの開く角度
    [SerializeField] private float _DoorSpeed = 1.0f;   // ドアの開くスピード(秒)

    private float _Variation;
    private float _Rot;
    private bool _RotStart;

    // Start is called before the first frame update
    void Start()
    {
        // ドア取得
        _DoorLeft = transform.Find("PivotL").gameObject;
        _DoorRight = transform.Find("PivotR").gameObject;

        // 1フレーム当たりの角度を初期化
        _Variation = _DoorRotAngle / _DoorSpeed;

        // 現在の回転角度
        _Rot = 0.0f;

        // 回転中かどうか
        _RotStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _RotStart = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _RotStart = false;
        }

        // ドアの開閉
        if(_RotStart)
        {
            _DoorLeft.transform.Rotate(0.0f, _Variation * Time.deltaTime, 0.0f);
            _DoorRight.transform.Rotate(0.0f, -_Variation * Time.deltaTime, 0.0f);

            _Rot += _Variation * Time.deltaTime;

            if (_Rot >= _DoorRotAngle)
            {
                _DoorLeft.transform.localRotation = Quaternion.Euler(0.0f, _DoorRotAngle, 0.0f);
                _DoorRight.transform.localRotation = Quaternion.Euler(0.0f, -_DoorRotAngle, 0.0f);
                
                _Rot = _DoorRotAngle;
            }
        }
        else
        {
            if(_Rot > 0.0f)
            {
                _DoorLeft.transform.Rotate(0.0f, -_Variation * Time.deltaTime, 0.0f);
                _DoorRight.transform.Rotate(0.0f, _Variation * Time.deltaTime, 0.0f);

                _Rot -= _Variation * Time.deltaTime;

                if (_Rot <= 0.0f)
                {
                    _DoorLeft.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    _DoorRight.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    
                    _Rot = 0.0f;
                }
            }
        }
    }
    
    // スイッチが押されたときに呼ばれる関数
    public void Interact(bool enable)
    {
        _RotStart = enable;
    }

}
