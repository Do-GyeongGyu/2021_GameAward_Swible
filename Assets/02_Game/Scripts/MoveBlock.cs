using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{

    public float length = 2.0f;

    private Rigidbody rb;
    private Vector3 Pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector3(Pos.x + Mathf.PingPong(Time.time, length), Pos.y , Pos.z));
    }

}
