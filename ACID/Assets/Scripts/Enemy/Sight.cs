using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public Patrol Patrol;
    public float SightDist;
    public LayerMask Layer;

    private bool _inSight;
    private Transform _target;

    private void Update()
    {
        if (_inSight)
        {
            var heading = _target.position - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            if (!Physics.Raycast(transform.position, direction, out var hit, SightDist, Layer))
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        _target = null;
        _inSight = false;
        Patrol.LosePlayer();
    }

    private void NewTarget(Transform target)
    {
        _target = target.transform;
        _inSight = true;
        Patrol.ChasePlayer(_target);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            print(other.name + " entered");
            var heading = other.transform.position - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            if (Physics.Raycast(transform.position, direction, out var hit, SightDist, Layer))
            {
                NewTarget(other.transform);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (_inSight)
            {
                Reset();
            }
        }
    }
}
