using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTrail : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brush")
        {
            Destroy(other.gameObject);
        }
    }
}
