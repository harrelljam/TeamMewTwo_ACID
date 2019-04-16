﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Animator _anim;
    private AudioSource _aud;
    public bool HasAudioSource;
    public bool isLocked;
    private Character _char;

    private void Start()
    {
        _char = Character.I;
        _anim = GetComponent<Animator>();
        if(HasAudioSource)
            _aud = GetComponent<AudioSource>();
    }

    public void KeyOpen()
    {
        isLocked = false;
        _anim.SetBool("character_nearby", true);
    }
    
    /// <summary>
    /// Triggers the door to open upon the character entering the sphere collider.
    /// If the isLocked bool is true, Characters hasKey bool must be true to
    /// prompt E to open message.
    /// </summary>
    /// <param name="c"></param>
    private void OnTriggerEnter(Collider c)
    {
        if ((c.gameObject.CompareTag("Player") && !isLocked || c.gameObject.CompareTag("AI")) && !isLocked)
        {
            if(HasAudioSource)
                _aud.Play();
            _anim.SetBool("character_nearby", true);
        }
    }
    /// <summary>
    /// Triggers the door to close upon character exiting sphere collider
    /// </summary>
    /// <param name="c"></param>
    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player") || c.gameObject.CompareTag("AI"))
        {
            _anim.SetBool("character_nearby", false);
        }
    }
}
