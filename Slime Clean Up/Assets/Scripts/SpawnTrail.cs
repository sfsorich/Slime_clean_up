using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrail : MonoBehaviour {
    [SerializeField]
    private GameObject brush;
    public List<GameObject> contacts;
    public Transform slimeParent;
    public Color colr;

    [SerializeField]
    private Collider last;
    public bool red;
    public bool green;
    public bool yellow;
    public bool blue;

    public float size = 1;
    public float val = 1;

    public bool working = true;

    private void Start()
    {
        brush = Instantiate<GameObject>(Resources.Load<GameObject>("Brush G"), this.transform);
        brush.SetActive(false);

        slimeParent = GameObject.Find("FloorSlime").transform;

        contacts = new List<GameObject>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!contacts.Contains(other.gameObject))
        {
            contacts.Add(other.gameObject);
        }

        if (other.tag == "Brush" && working)
        {
            if (other.GetComponent<DeleteBrush>().touched)
                StartCoroutine(ShrinkDie(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (contacts.Contains(other.gameObject))
        {
            contacts.Remove(other.gameObject);
        }

        //if (other.tag == "Brush")
        //{
            //Destroy(other.gameObject);
            //GameObject b = Instantiate<GameObject>(brush, this.transform);
            //b.transform.parent = null;
            //last = b.GetComponent<Collider>();
            //Destroy(other);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        /*if (last != null && !contacts.Contains(last.gameObject) && working)
        {
            GameObject b = Instantiate<GameObject>(brush, this.transform);
            b.transform.parent = slimeParent;
            last = b.GetComponent<Collider>();
            b.GetComponent<DeleteBrush>().UpdateSize(size);
            b.GetComponent<DeleteBrush>().val = val;
            b.SetActive(true);
        }*/

        
    }

    private void Update()
    {
        brush.GetComponentInChildren<SpriteRenderer>().color = colr;
        contacts.RemoveAll(GameObject => GameObject == null);
        
        if ((last == null && working) || (last != null && !contacts.Contains(last.gameObject) && working))
        {
            GameObject b = Instantiate<GameObject>(brush, this.transform);
            b.transform.parent = slimeParent;
            last = b.GetComponent<Collider>();
            b.GetComponent<DeleteBrush>().UpdateSize(size);
            b.GetComponent<DeleteBrush>().val = val;
            b.SetActive(true);
        }
        //print(last.name);
    }

    private IEnumerator ShrinkDie(GameObject o)
    {
        //o.GetComponent<Collider>().enabled = false;
        //contacts.Remove(o);
        //GameObject b = Instantiate<GameObject>(brush, o.transform);
        //b.transform.parent = slimeParent;
        //last = b.GetComponent<Collider>();
        //b.GetComponent<DeleteBrush>().size = size;

        //for (int i = 0; i < 60; i++)
        //{
        //  o.transform.localScale = Vector3.Lerp(o.transform.localScale, Vector3.zero, 0.15f);
        //
        //yield return new WaitForEndOfFrame();
        //}

        //Destroy(o);

        o.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(o.GetComponentInChildren<SpriteRenderer>().color, colr, 0.5f);

        if(o.GetComponent<DeleteBrush>().size < size)
        {
            o.GetComponent<DeleteBrush>().UpdateSize(size);
        }
        

        yield return new WaitForEndOfFrame();
    }
}
