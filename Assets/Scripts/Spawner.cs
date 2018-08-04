using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    float timer;
    public GameObject Enemy1;
    public float spawnDelay = 2.0f;

	// Use this for initialization
	void Start ()
    {
    
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

		if(timer >= spawnDelay)
        {
            Instantiate(Enemy1,transform.position,transform.rotation);
            timer = 0;
        }
	}
}
