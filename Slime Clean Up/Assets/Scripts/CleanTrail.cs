using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTrail : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brush")
        {
            //StartCoroutine(ShrinkSucc(other.gameObject));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Brush")
        {
            other.GetComponent<DeleteBrush>().size -= 0.25f;
            other.transform.position = Vector3.Lerp(other.transform.position, this.transform.position, 0.075f);
        }
    }

    private IEnumerator ShrinkSucc(GameObject o)
    {
        o.GetComponent<Collider>().enabled = false;
        for (int i = 0; i < 60; i++)
        {
            o.transform.localScale = Vector3.Lerp(o.transform.localScale, Vector3.zero, 0.15f);
            o.transform.position = Vector3.Lerp(o.transform.position, this.transform.position, 0.25f);

            yield return new WaitForEndOfFrame();
        }

        Destroy(o);
        yield return new WaitForEndOfFrame();
    }
}
