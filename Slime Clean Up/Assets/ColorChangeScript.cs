using ProBuilder.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeScript : MonoBehaviour {
    private Transform Janitor;
    private pb_Object floor;
    private Color[] c;
    // Use this for initialization
    void Start () {
        Janitor = GameObject.FindGameObjectWithTag("Janitor").transform;
        floor = this.GetComponent<pb_Object>();
        floor.ToMesh();
        c = new Color[floor.vertexCount];
        for(int i = 0; i < floor.vertexCount; i++)
        {
            c[i] = Color.gray;
        }
        
        floor.SetColors(c);
        floor.Refresh();

        //StartCoroutine(ColorPlay());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*private IEnumerator ColorPlay()
    {

        for(int i = 0; i < floor.vertexCount; i++)
        {
            floor.vertices.
            c[i] = Color.green;

            floor.SetColors(c);
            floor.Refresh();

            yield return new WaitForEndOfFrame();
        }
    }*/
}
