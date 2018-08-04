using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Slider healthSlider;
    public Slider manaSlider;
    public Slider staminaSlider;

    PlayerBaseScript playerBaseScript;

    void Awake()
    {
       playerBaseScript = FindObjectOfType<PlayerBaseScript>();
    }
    // Use this for initialization
    void Start ()
    {
        SetHpSliderMax();
        SetMpSliderMax();
        SetSpSliderMax();

    }
	
	// Update is called once per frame
	void Update ()
    {
        SetHpSlider();
        SetMpSlider();
        SetSpSlider();
	}

    public void SetHpSliderMax()
    {
        healthSlider.maxValue = playerBaseScript.pfMaxHealth;
    }
    public void SetMpSliderMax()
    {
        manaSlider.maxValue = playerBaseScript.pfMaxMana;
    }
    public void SetSpSliderMax()
    {
        staminaSlider.maxValue = playerBaseScript.pfMaxStamina;
    }

    public void SetHpSlider()
    {
        healthSlider.value = playerBaseScript.pfHealth;
    }

    public void SetMpSlider()
    {
        manaSlider.value = playerBaseScript.pfMana;
    }

    public void SetSpSlider()
    {
        staminaSlider.value = playerBaseScript.pfStamina;
    }



}
