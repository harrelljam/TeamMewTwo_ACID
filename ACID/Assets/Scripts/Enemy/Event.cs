using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Event : MonoBehaviour
{
    public GameObject[] EventObjects;
    public String Message;


    // Start is called before the first frame update
    public void CallTrigger()
    {
        foreach (GameObject obj in EventObjects)
        {
            obj.SetActive(true);
        }
        GetComponent<Patrol>().enabled = true;

        if (Message != "")
        {
            Character.I.DisplayMessageTimed(Message, 3f);
        }
    }
}
