using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBrush : MonoBehaviour
{
    public bool touched = false;
    public float size = 1;
    private float t = 0;
    private Transform sprite;

    private void Start()
    {

        sprite = this.transform.GetChild(0);
        sprite.localScale *= 0.5f;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Painter")
        {
            touched = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Brush")
        {
            print("isbrush");
            float othrSize = other.GetComponent<DeleteBrush>().size;
            if (othrSize <= size)
            {
                print("Size If");
                size += othrSize;
                Destroy(other.gameObject);
            }
        }
    }

    private void Update()
    {
        t += Time.deltaTime;
        sprite.localScale = Vector3.Lerp(sprite.localScale, new Vector3(size, size, size), t);
    }
}
