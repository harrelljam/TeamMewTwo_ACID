using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Animator _anim;
    private AudioSource _aud;
    public bool HasAudioSource;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        if(HasAudioSource)
            _aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            if(HasAudioSource)
                _aud.Play();
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
