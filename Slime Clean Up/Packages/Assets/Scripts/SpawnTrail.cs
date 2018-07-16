using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrail : MonoBehaviour {
    [SerializeField]
    private GameObject brush;
    private List<GameObject> contacts;

    [SerializeField]
    //private Color colr;
    private Collider last;
    public bool red;
    public bool green;
    public bool yellow;
    public bool blue;

    private void Start()
    {
        if(red)
            brush = Resources.Load("Brush R") as GameObject;
        if (green)
            brush = Resources.Load("Brush G") as GameObject;
        if (yellow)
            brush = Resources.Load("Brush Y") as GameObject;
        if (blue)
            brush = Resources.Load("Brush B") as GameObject;

        contacts = new List<GameObject>();

        brush.GetComponentInChildren<SpriteRenderer>().color = this.transform.parent.GetComponentInChildren<SpriteRenderer>().color;
        GameObject b = Instantiate<GameObject>(brush, this.transform);
        b.transform.parent = null;
        last = b.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!contacts.Contains(other.gameObject))
        {
            contacts.Add(other.gameObject);
        }

        if (other.tag == "Brush")
        {
            if(other.GetComponent<DeleteBrush>().touched)
                Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (contacts.Contains(other.gameObject))
        {
            contacts.Remove(other.gameObject);
        }

        if (other.tag == "Brush")
        {
            //Destroy(other.gameObject);
            //GameObject b = Instantiate<GameObject>(brush, this.transform);
            //b.transform.parent = null;
            //last = b.GetComponent<Collider>();
            //Destroy(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!contacts.Contains(last.gameObject))
        {
            GameObject b = Instantiate<GameObject>(brush, this.transform);
            b.transform.parent = null;
            last = b.GetComponent<Collider>();
        }
    }

    private void Update()
    {
        //colr = brush.GetComponentInChildren<SpriteRenderer>().color;
        if(last == null)
        {
            GameObject b = Instantiate<GameObject>(brush, this.transform);
            b.transform.parent = null;
            last = b.GetComponent<Collider>();
        }
        //print(last.name);
    }
}
