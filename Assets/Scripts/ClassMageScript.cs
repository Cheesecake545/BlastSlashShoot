using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassMageScript : MonoBehaviour {

    //Modifier  values + - 
    public float pfHealthRegen = 0.0f;
    public float pfManaRegen = 0.0f;
    public float pfStaminaRegen = 0.0f;

    public float pfFireballCost = -30.0f;

    public GameObject Fireball;
    public GameObject StartPoint;

    void Awake()
    {

    }

    private void Update()
    {

    }

    public void Attack()
    {   
        Instantiate(Fireball, StartPoint.transform.position, StartPoint.transform.rotation);
    }
}
