using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float sightDistance;
    public Transform origin;
    public LayerMask _layer;
    private GameObject _target;
    private Character _char;

    void Start()
    {
        _char = Character.I;
    }
    // Update is called once per frame
    void Update()
    {
        if (FindItem())
        {
            //Looking at a key - display message, allow input.
            if (_target.tag.Equals("Key"))
            {
                _char.DisplayMessage("Press E to get keycard");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _char.hasKey = true;
                    Destroy(_target);
                    _char.DestroyMessage();
                }
            }
            
            //Looking at a terminal
            if (_target.tag.Equals("Terminal"))
            {
            	_char.DisplayMessage("Press E to access terminal");
            	if (Input.GetKeyDown(KeyCode.E))
            	{
            		Debug.Log("Using a terminal");
            	}
            }
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
            _target = hit.transform.gameObject;
            return true;
        }
        return false;
    }
}
