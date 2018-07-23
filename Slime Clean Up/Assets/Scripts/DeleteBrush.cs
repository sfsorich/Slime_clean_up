using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBrush : MonoBehaviour
{
    public bool touched = false;
    public float size = 1;
    public float val = 1;
    private float t = 0;
    private Transform sprite;

    private void Start()
    {

        sprite = this.transform.GetChild(0);
        sprite.localScale *= 0.5f;
    }

    public void Kill(){
        Destroy(this.gameObject);
    }

    IEnumerator KillCo(){
        for (int i = 10; i > 0; i--){
            this.transform.localScale *= 0.5f;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
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
        if (touched && other.tag == "Brush")
        {
            if (!other.GetComponent<DeleteBrush>().touched)
            {
               other.GetComponent<DeleteBrush>().Kill();
            }    
        }
    }

    private void Update()
    {
        //t += Time.deltaTime;
        sprite.localScale = Vector3.Lerp(sprite.localScale, new Vector3(size, size, size), 0.05f);

        if (size <= 0.1) 
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateSize(float s)
    {
        size = s;
        this.GetComponent<CapsuleCollider>().radius = size / 5;
    }
}
