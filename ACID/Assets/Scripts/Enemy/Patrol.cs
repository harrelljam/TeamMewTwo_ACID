// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour
{

    public float WalkSpeed;
    public float ChaseSpeed;
    public float PauseTime;
    public Animator Anim;
    public Transform[] Points;
    public AudioClip[] RobotScreams;
    public Transform DeathSpot;
    
    private int _destPoint = 0;
    private NavMeshAgent _agent;
    private Coroutine _wait;
    private Coroutine _hunt;
    private bool _waiting = false;
    private bool _hunting = false;
    private bool _chasing = false;
    private Transform _target;
    private AudioSource _source;


    private void Start ()
    {
        _source = this.GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        _agent.autoBraking = false;

        GotoNextPoint();
    }


    private void GotoNextPoint() {
        // Returns if no points have been set up
        if (Points.Length == 0)
            return;
        _agent.isStopped = false;
        // Set the agent to go to the currently selected destination.
        _agent.destination = Points[_destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        _destPoint = (_destPoint + 1) % Points.Length;
        Anim.SetBool("moving", true);
    }

    public void ChasePlayer(Transform player)
    {
        if (_waiting)
        {
            StopCoroutine(_wait);
            _waiting = false;
        }
        if(_hunting)
        {
            StopCoroutine(_hunt);
            _hunting = false;
        }
        
        if (!_chasing)
        {
            _chasing = true;
            _target = player;
            _agent.speed = ChaseSpeed;
            _agent.isStopped = false;
            Anim.SetBool("chasing", true);
            Anim.SetBool("moving", true);
            _source.clip = RobotScreams[Random.Range(0, RobotScreams.Length)];
            _source.Play();
        }
    }
    public void LosePlayer()
    {
        _hunting = true;
        _hunt = StartCoroutine(WaitHunt(3f));
    }

    private void Update () {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f && !_waiting && !_chasing)
        {
            _agent.isStopped = true;
            _wait = StartCoroutine(WaitHere(PauseTime));
        }

        if (_chasing)
        {
            float dist = Vector3.Distance(_target.position, transform.position);
            if (dist < 2f)
            {
                _chasing = false;
                _target.gameObject.GetComponent<Character>().Death(DeathSpot);
                _agent.isStopped = true;
                Anim.SetBool("moving", false);
                Anim.SetBool("chasing", false);
                Anim.SetTrigger("JumpScare");
                _source.clip = RobotScreams[Random.Range(0, RobotScreams.Length)];
                _source.Play();
            }
            else
            {
                _agent.destination = _target.position;
            }
        }
    }

    private IEnumerator WaitHere(float seconds)
    {
        _waiting = true;
        Anim.SetBool("moving", false);
        yield return new WaitForSeconds(seconds);
        if (!_chasing)
        {
            _waiting = false;
            GotoNextPoint();   
        }
    }
    private IEnumerator WaitHunt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _hunting = false;
        _chasing = false;
        _target = null;
        _agent.speed = WalkSpeed;
        Anim.SetBool("chasing", false);
        _agent.isStopped = true;
        if(!_waiting)
            _wait = StartCoroutine(WaitHere(PauseTime));
    }
}
