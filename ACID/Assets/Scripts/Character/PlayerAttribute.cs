using System.Collections;
using UnityEngine;
using UnityEditor;

public class PlayerAttribute : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public Animator flashAnim;
    public float MaxHealth = 100;
    public float CurrentHealth;
    public static PlayerAttribute Instance;
    static float val;
    public int Ship;

    public bool hurtMe = false;

    // initiation of health
    private void Start()
    {
        Instance = this;
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
    }


    //Pulls maxHealth from a static instance
    public float getHealth()
    {
        return CurrentHealth;
    }


    public void takingDamage(float damage)
    {
        CurrentHealth -= damage;
        flashAnim.SetTrigger("Ouch");
    }
}