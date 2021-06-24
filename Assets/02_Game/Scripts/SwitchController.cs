using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private bool _HoldButton = false;  // 押し続けるボタンかどうか false : 一度押すと戻らない   true : ボタンから離れると戻る
    [SerializeField] private bool _IsPress;     // ボタンが押されているか

    public GameObject _InteractiveObject;

    
    // Start is called before the first frame update
    void Start()
    {
        _IsPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_HoldButton)
        {
            _IsPress = false;
        }

        RaycastHit hit;
        if (Physics.BoxCast(transform.position, Vector3.one * 0.5f, transform.up, out hit, transform.rotation, 1.0f))
        {
            if(hit.transform.tag == "Player")
            {
                _IsPress = true;
                Debug.Log("hit");
            }
        }

        //if(_IsPress)
        //{
        //    transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        //}
        //else
        //{
        //    transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
        //}

        Debug.Log(_IsPress);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up * 10.0f);
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
