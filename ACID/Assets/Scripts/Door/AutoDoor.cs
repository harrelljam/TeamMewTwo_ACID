using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Animator _anim;
    private AudioSource _aud;
    public bool HasAudioSource;
    public bool isLocked;
    public bool characterNearby;
    private Character _char;

    private void Start()
    {
        _char = Character.I;
        _anim = GetComponent<Animator>();
        if(HasAudioSource)
            _aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (characterNearby)
        {
            if(Input.GetKeyDown(KeyCode.E) && isLocked && _char.hasKey)
            {
                isLocked = false;
                if(HasAudioSource)
                    _aud.Play();
                _anim.SetBool("character_nearby", true);
                _char.DestroyMessage();
            }
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player") && isLocked)
        {
            characterNearby = true;
            if (_char.hasKey)
            {
                _char.DisplayMessage();
                _char.messageText.text = "Press E to unlock door";
            }
            else
            {
                _char.DisplayMessage();
                _char.messageText.text = "you need a key";
            }
        }

        else if (c.gameObject.CompareTag("Player") && !isLocked)
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
            characterNearby = false;
            _anim.SetBool("character_nearby", false);
        }
    }
}
