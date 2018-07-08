using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTrail : MonoBehaviour {
    private Transform janitor;

    private void Start()
    {
        janitor = GameObject.FindWithTag("Janitor").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brush")
        {
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        this.transform.position = janitor.position + new Vector3(0, -0.45f, -0.8f);
    }
}
