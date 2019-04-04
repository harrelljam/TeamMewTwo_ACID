using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    public AudioClip[] Steps;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    
    
    public void PlayStep()
    {
        _source.clip = Steps[Random.Range(0, Steps.Length)];
        _source.Play();
    }

}
