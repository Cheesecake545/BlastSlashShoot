using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

	Transform player;
    //PlayerHealth playerHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;
    MeshRenderer mesh;
    SphereCollider spherecollider;
    

    public float EnemyHealth;
    public float EnemyDamage;
    float fadeTime = 2.0f;
    float fAttackTimer = 0.0f;
    float fAttackDelay = 2.0f;

    Color color;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
        spherecollider = GetComponent<SphereCollider>();
        color = mesh.material.color;
        color.a = 0;
        EnemyHealth = 100.0f;
        EnemyDamage = 20.0f;
    }


    void Update()
    {
        if(EnemyHealth > 0/*&& playerHealth.currentHealth > 0*/)
        {
        nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
            spherecollider.enabled = false;
            // anim.SetTrigger("IsDead");
            mesh.material.color = Color.Lerp(mesh.material.color, color, fadeTime * Time.deltaTime);

            if (mesh.material.color.a <= 0.1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        fAttackTimer += Time.deltaTime;
        if (other.tag == "Player")
        {
            if (fAttackTimer > fAttackDelay)
            {
                anim.SetTrigger("IsAttack");
                other.GetComponent<PlayerBaseScript>().pfHealth -= EnemyDamage;
                fAttackTimer = 0.0f;
            }
        }
    }
}
