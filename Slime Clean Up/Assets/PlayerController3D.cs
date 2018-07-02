using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour {
    public float speed = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody>().velocity += Vector3.fwd * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            this.GetComponent<Rigidbody>().velocity += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.GetComponent<Rigidbody>().velocity += Vector3.back * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.GetComponent<Rigidbody>().velocity += Vector3.right * speed;
        }
    }
}
