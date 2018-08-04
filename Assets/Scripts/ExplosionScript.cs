using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    float fadeTime = 2.0f;
    private Color color;
    private MeshRenderer mesh;

	// Use this for initialization
	void Start ()
    {
        mesh = this.GetComponent<MeshRenderer>();
        color = mesh.material.color;
        color.a = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        mesh.material.color = Color.Lerp(mesh.material.color, color, fadeTime * Time.deltaTime);

        if(mesh.material.color.a <= 0.1)
        {
            Destroy(this.gameObject);
        }
    }
}
