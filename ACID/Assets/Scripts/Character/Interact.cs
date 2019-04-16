using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public static Interact I;
    
    public GameObject flashlight;
    public float sightDistance;
    public Transform origin;
    public LayerMask _layer;
    private GameObject _target;
    private AutoDoor _targetDoor;
    private bool _targetSpotted;
    
    private Character _char;
    private PlayerAttribute _attr;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        _attr = PlayerAttribute.I;
        _char = Character.I;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && _attr.CurrentBattery > 0)
        {
            flashlight.active = !flashlight.active;
        }

        if (_attr.CurrentBattery <= 0)
        {
            flashlight.SetActive(false);
        }
        if (FindItem())
        {
            //Looking at a key - display message, allow input.
            if (_target.tag.Equals("Key"))
            {
                _char.DisplayMessage("Press E to get keycard");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _char.hasKey = true;
                    _targetSpotted = false;
                    Destroy(_target);
                    _char.DestroyMessage();
                }
            }
            if (_target.tag.Equals("Light"))
            {
                _char.DisplayMessage("Press E to get flashlight");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _char.DestroyMessage();
                    _char.Flightlight.SetActive(true);
                    _char.DisplayMessageTimed("Press F to toggle flashlight", 3f);
                    Destroy(_target);
                }
            }
            if (_target.tag.Equals("Battery"))
            {
                _char.DisplayMessage("Press E to grab battery");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _char.DestroyMessage();
                    _attr.RefillBattery();
                    Destroy(_target);
                }
            }
            if (_target.tag.Equals("Health"))
            {
                _char.DisplayMessage("Press E to grab healthpack");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _char.DestroyMessage();
                    _attr.RefillHealth();
                    Destroy(_target);
                }
            }
            if (_target.tag.Equals("Door"))
            {
                if (_targetDoor == null)
                {
                    _targetDoor = _target.GetComponent<AutoDoor>();
                }
                
                if(_char.hasKey && _targetDoor.isLocked)
                    _char.DisplayMessage("Press E to open door");
                else if (_targetDoor.isLocked)
                    _char.DisplayMessage("You need a keycard");
                
                if (Input.GetKeyDown(KeyCode.E) && _char.hasKey && _targetDoor.isLocked)
                {
                    _targetDoor.KeyOpen();
                    _char.hasKey = false;
                    _targetSpotted = false;
                    _target = null;
                    _targetDoor = null;
                    _char.DestroyMessage();
                }
            }
        }
        else if (_targetSpotted)
        {
            _target = null;
            _targetSpotted = false;
            _char.DestroyMessage();
        }
    }
    
    /// <summary>
    /// Casts a ray in the forward direction of Canvas searching for object in the _layer Layer.
    /// If found, sets the _target to the object found and returns true.
    /// </summary>
    /// <returns></returns>
    private bool FindItem()
    {
        if (Physics.Raycast(origin.position, origin.forward, out var hit, sightDistance, _layer))
        {
            _targetSpotted = true;
            _target = hit.transform.gameObject;
            return true;
        }
        return false;
    }
}