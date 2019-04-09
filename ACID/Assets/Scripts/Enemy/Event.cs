using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Event : MonoBehaviour
{
    public GameObject[] EventObjects;


    // Start is called before the first frame update
    public void CallTrigger()
    {
        foreach (GameObject obj in EventObjects)
        {
            obj.SetActive(true);
        }
        GetComponent<Patrol>().enabled = true;
    }
}
