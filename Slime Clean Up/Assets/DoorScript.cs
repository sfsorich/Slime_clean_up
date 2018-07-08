using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    private Transform doorL;
    private Transform doorR;
    private Vector3 origL;
    private Vector3 origR;
    private Vector3 newR;
    private Vector3 newL;

    // Use this for initialization
    void Start () {
        doorL = this.transform.Find("DoorL");
        doorR = this.transform.Find("DoorR");

        origL = new Vector3(doorL.localPosition.x, doorL.localPosition.y, doorL.localPosition.z);
        origR = new Vector3(doorR.localPosition.x, doorR.localPosition.y, doorR.localPosition.z);
        newL = new Vector3(origL.x, origL.y, origL.z - 1.45f);
        newR = new Vector3(origR.x, origR.y, origR.z + 1.45f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Janitor")
        {
            StopAllCoroutines();
            StartCoroutine(Open(30));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Janitor")
        {
            StopAllCoroutines();
            StartCoroutine(Close(10));
        }
    }

    private IEnumerator Open(int frames)
    {
        for (float i = 0; i < frames; i++)
        {
            doorL.localPosition = Vector3.Lerp(origL, newL, i / frames);
            doorR.localPosition = Vector3.Lerp(origR, newR, i / frames);
            yield return new WaitForEndOfFrame();
        }
        doorL.localPosition = newL;
        doorR.localPosition = newR;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator Close(int frames)
    {
        for (float i = 0; i < frames; i++)
        {
            doorL.localPosition = Vector3.Lerp(newL, origL, i / frames);
            doorR.localPosition = Vector3.Lerp(newR, origR, i / frames);
            yield return new WaitForEndOfFrame();
        }
        doorL.localPosition = origL;
        doorR.localPosition = origR;
        yield return new WaitForEndOfFrame();
    }
}
