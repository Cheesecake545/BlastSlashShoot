using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour {

   
    Rigidbody fireballRB;

    float speed = 10.0f;
    float range = 50.0f;
    public float damage = 50.0f;

    Vector3 movement;
    Vector3 startPos;

    public GameObject explosion;
    

    void Awake()
    {
        fireballRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        startPos = transform.position;
    }


    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position,startPos) >= range)
        {

            Destroy(this.gameObject);
        }

        movement = transform.forward;
        movement = movement.normalized * speed * Time.deltaTime;
        fireballRB.MovePosition(transform.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement>().EnemyHealth -= damage;
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }
}
