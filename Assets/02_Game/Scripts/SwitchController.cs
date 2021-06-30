using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private bool _HoldButton = false;  // 押し続けるボタンかどうか false : 一度押すと戻らない   true : ボタンから離れると戻る
    private bool _IsPress;     // ボタンが押されているか

    public GameObject _InteractiveObject;

    
    // Start is called before the first frame update
    void Start()
    {
        _IsPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        _InteractiveObject.GetComponent<DoorController>().Interact(_IsPress);

        RaycastHit hit;
        if (Physics.BoxCast(transform.position + new Vector3(0.0f, -1.0f, 0.0f), Vector3.one * 1.0f, transform.up, out hit, transform.rotation, 0.5f))
        {
            if(hit.transform.tag == "Player")
            {
                _IsPress = true;
                Debug.Log("hit");
            }
        }
        else
        {
            if (_HoldButton)
            {
                _IsPress = false;
            }
        }

        if (_IsPress)
        {
            transform.localPosition = new Vector3(0.0f, 0.01f, 0.0f);
        }
        else
        {
            transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
        }

        Debug.Log(_IsPress);

    }
}
