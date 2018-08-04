using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    public GameObject Fireball;
    public GameObject StartPoint;

    public float fTimer;
    public float fTimeBetweenFire = 1.0f;
    Animator playerAnim;

    // Use this for initialization
    void Start ()
    {
        playerAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        

        fTimer += Time.deltaTime;

       
          if(Input.GetButton("Fire1") && fTimer >= fTimeBetweenFire && Time.timeScale != 0)
        {   playerAnim.SetTrigger("tAttacking");
            Instantiate(Fireball,StartPoint.transform.position,StartPoint.transform.rotation);
            fTimer = 0f;
           // playerAnim.ResetTrigger("tAttacking");
        }

	}
}
