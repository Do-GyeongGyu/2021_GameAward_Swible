using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMgr : MonoBehaviour
{
    [SerializeField]
    private GameObject effectJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void EffectJump()
    {
        GameObject effect = Instantiate(effectJump, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);
    }
}
