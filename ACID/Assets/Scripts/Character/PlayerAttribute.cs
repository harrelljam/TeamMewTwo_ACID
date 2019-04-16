using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour
{
    public static PlayerAttribute I;
    
    public SceneLoader sceneLoader;
    public Animator flashAnim;
    public float MaxHealth = 100;
    public float CurrentHealth;
    public float MaxBattery = 100;
    public float CurrentBattery;
    public Slider HealthSlider;
    public Slider BatterySlider;
    static float val;
    public int Ship;

    public bool hurtMe = false;

    // initiation of health
    private void Awake()
    {
        I = this;
        CurrentHealth = MaxHealth;
    }

    //update called once per frame
    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            //Destroy(gameObject);
            sceneLoader.LoadScene(Ship);
        }

        if (hurtMe)
        {
            takingDamage(10f);
            hurtMe = false;
        }

        CurrentBattery -= Time.deltaTime;
        
        HealthSlider.value = CurrentHealth / MaxHealth;
        BatterySlider.value = CurrentBattery / MaxBattery;
    }

    public void RefillHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public void RefillBattery()
    {
        CurrentBattery = MaxBattery;
    }

    public void takingDamage(float damage)
    {
        CurrentHealth -= damage;
        flashAnim.SetTrigger("Ouch");
    }
}