using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            _anim.SetBool("character_nearby", true);
        }
    }
    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            _anim.SetBool("character_nearby", false);
        }
    }
}
