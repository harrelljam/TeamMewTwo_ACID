using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public Patrol Patrol;
    public float SightDist;
    public LayerMask Layer;

    private bool _inRange;
    private bool _spotted;
    private Transform _target;

    private void Update()
    {
        if (_inRange && !_spotted)
        {
            if (SightRay(_target))
            {
                _spotted = true;
                Chase(_target);
            }
        }

        if (_spotted)
        {
            if (!SightRay(_target))
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        _spotted = false;
        Patrol.LosePlayer();
    }

    private void Chase(Transform target)
    {
        Patrol.ChasePlayer(_target);
    }

    private bool SightRay(Transform other)
    {
        var from = transform.position;
        var heading = other.transform.position - from;
        var distance = heading.magnitude;
        var direction = heading / distance;
        var sightRay = new Ray(from, direction);
        if (Physics.Raycast(sightRay, out var hit, SightDist))
        {
            if (hit.transform.tag.Equals("Player"))
            {
                return true; 
            }
            return false;
        }
        return false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _inRange = true;
            _target = other.transform;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _spotted = false;
            _inRange = false;
            _target = null;
        }
    }
}
