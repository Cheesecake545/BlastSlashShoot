using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseScript : MonoBehaviour
{
    public ClassMageScript MageScript = null;

    public float pfHealth;
    public float pfMana;
    public float pfStamina;

    public float pfMaxHealth;
    public float pfMaxMana;
    public float pfMaxStamina;

    public float pfHealthRegen;
    public float pfManaRegen;
    public float pfStaminaRegen;

    float fRegenTimer;
    public float fRegenDelay = 1.0f;

    float fAttackTimer;
    public float fAttackDelay = 1.0f;

    bool bIsDead;

    Animator playerAnim;

    private void Awake()
    {
        pfMaxHealth = 100.0f;
        pfMaxMana = 100.0f;
        pfMaxStamina = 100.0f;

        pfHealthRegen = 0.0f;
        pfManaRegen = 10.0f;
        pfStaminaRegen = 10.0f;

        pfHealth = pfMaxHealth;
        pfMana = pfMaxMana;
        pfStamina = pfMaxStamina;

        MageScript = GetComponent<ClassMageScript>();
       
        if(MageScript != null)
        {
            SetHealthRegen(MageScript.pfHealthRegen);
            SetManaRegen(MageScript.pfManaRegen);
            SetStaminaRegen(MageScript.pfStaminaRegen);
        }

        playerAnim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Regeneration();

        fAttackTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fAttackTimer >= fAttackDelay && Time.timeScale != 0 && pfMana >= (MageScript.pfFireballCost * -1))
        {
            Attack();
            fAttackTimer = 0.0f;
        }

        if(pfHealth <= 0 && !bIsDead)
        {
            SetDead();
        }
    }

    

    public void SetHealth(float _health)
    {
        pfHealth += _health;
    }

    public void SetMana(float _mana)
    {
        pfMana += _mana;
    }

    public void SetStamina(float _stamina)
    {
        pfStamina += _stamina;
    }

    public void SetHealthRegen(float _healthRegen)
    {
        pfHealthRegen += _healthRegen;
    }

    public void SetManaRegen(float _manaRegen)
    {
        pfManaRegen += _manaRegen;
    }

    public void SetStaminaRegen(float _StaminaRegen)
    {
        pfStaminaRegen += _StaminaRegen;
    }

    //Regenerates every second and also checks if goes beyond max value
    void Regeneration()
    {
        fRegenTimer += Time.deltaTime;

        if (fRegenTimer > fRegenDelay)
        {
            pfHealth += pfHealthRegen;
            pfMana += pfManaRegen;
            pfStamina += pfStaminaRegen;
            fRegenTimer = 0.0f;
        }

        if (pfHealth > pfMaxHealth)
        {
            pfHealth = pfMaxHealth;
        }

        if (pfMana > pfMaxMana)
        {
            pfMana = pfMaxMana;
        }

        if (pfStamina > pfMaxStamina)
        {
            pfStamina = pfMaxStamina;
        }
    }

    void Attack()
    {
        if(MageScript != null)
        {
            MageScript.Attack();
            playerAnim.SetTrigger("tAttacking");
            SetMana(MageScript.pfFireballCost);
        }
    }

    void SetDead()
    {
        playerAnim.SetTrigger("tDead");
        GetComponent<MovementPlayer>().enabled = false;
        this.enabled = false;
    }
}
