using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody2D>().velocity += Vector2.up * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            this.GetComponent<Rigidbody2D>().velocity += Vector2.left * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.GetComponent<Rigidbody2D>().velocity += Vector2.down * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.GetComponent<Rigidbody2D>().velocity += Vector2.right * speed;
        }
    }
}
