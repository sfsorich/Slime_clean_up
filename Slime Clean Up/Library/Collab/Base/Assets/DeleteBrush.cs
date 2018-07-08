using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBrush : MonoBehaviour {
    public bool touched = false;
    private Transform sprite;

    private void Start()
    {

        sprite = this.transform.GetChild(0);
        sprite.localScale *= 0.1f;
        StartCoroutine(Grow());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Painter")
        {
            touched = true;
        } 
    }

    private IEnumerator Grow()
    {
        for(int i = 1; i < 11; i++)
        {
            sprite.localScale = (Vector3.one / 10) * i;

            yield return new WaitForEndOfFrame();
        }
    }
}
