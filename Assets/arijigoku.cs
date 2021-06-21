using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class arijigoku : MonoBehaviour
{

    public Transform objA;
    public Transform objB;

    public float speed = 0.01F;

    private float distanceAB;

    void Start()
    {
  
    }

    void Update()
    {

        distanceAB = Vector3.Distance(objA.position, objB.position);
        Debug.Log(distanceAB);

        if (distanceAB <= objB.transform.localScale.x/ 2)
        {
            // 現在の位置
            float present_Location = (Time.deltaTime * speed) / distanceAB;
            //Lerp処理
            transform.position = Vector3.Lerp(objA.position, objB.position, present_Location);
        }
       
    }
}

