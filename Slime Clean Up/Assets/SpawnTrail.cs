using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrail : MonoBehaviour {
    private GameObject brush;
    private Collider last;

    private void Start()
    {
        brush = Resources.Load<GameObject>("Brush");
        GameObject b = Instantiate<GameObject>(brush, this.transform);
        b.transform.parent = null;
        last = b.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brush")
        {
            if(other.GetComponent<DeleteBrush>().touched)
                Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Brush")
        {
            //Destroy(other.gameObject);
            GameObject b = Instantiate<GameObject>(brush, this.transform);
            b.transform.parent = null;
            last = b.GetComponent<Collider>();
            //Destroy(other);
        }
    }

    private void Update()
    {
        if(last == null)
        {
            GameObject b = Instantiate<GameObject>(brush, this.transform);
            b.transform.parent = null;
            last = b.GetComponent<Collider>();
        }
        print(last.name);
    }
}
