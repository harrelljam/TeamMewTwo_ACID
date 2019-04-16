using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public  float HealthAmount ;
    public bool InFire= false;
    private PlayerAttribute player;
    private float time;
    public float interval;

    private void Update()
    {
        time = time + Time.deltaTime;
        if (InFire && (time > interval))
        {
           
            time = 0;
            player.takingDamage(HealthAmount);
            //yield WaitForSeconds(Interval);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InFire = true;
            player = other.gameObject.GetComponent<PlayerAttribute>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            InFire = false;
        }
    }

}
