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
            MonoBehaviour targetMono;
            if (_target.tag.Equals("Key"))
            {
                _char.DisplayMessage();
                _char.messageText.text = "Press E to get keycard";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _char.hasKey = true;
                    Destroy(_target);
                    _char.DestroyMessage();
                }
            }
        }
    }
    
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
