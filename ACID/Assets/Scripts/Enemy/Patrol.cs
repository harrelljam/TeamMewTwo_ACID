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
    private int _destPoint = 0;
    private NavMeshAgent _agent;
    private bool _waiting = false;
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
        _target = player;
        _chasing = true;
        _agent.destination = player.position;
        _agent.speed = ChaseSpeed;
        Anim.SetBool("chasing", true);
        Anim.SetBool("moving", true);
        _source.clip = RobotScreams[Random.Range(0, RobotScreams.Length)];
        _source.Play();
    }
    public void LosePlayer()
    {
        _chasing = false;
        _target = null;
        _agent.speed = WalkSpeed;
        Anim.SetBool("chasing", false);
        Anim.SetBool("moving", false);
        _agent.isStopped = true;
        StartCoroutine(WaitHere(PauseTime));
    }

    private void Update () {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f && !_waiting && !_chasing)
        {
            _agent.isStopped = true;
            StartCoroutine(WaitHere(PauseTime));
            _waiting = true;   
        }

        if (!_agent.pathPending && _agent.remainingDistance < 0.5f && _chasing)
        {
            print("you lose");
        }
        else if (!_agent.pathPending && _agent.remainingDistance < 2f && _chasing)
        {
            _agent.destination = _target.position;
        }
    }

    private IEnumerator WaitHere(float seconds)
    {
        Anim.SetBool("moving", false);
        yield return new WaitForSeconds(seconds);
        if (!_chasing)
        {
            _waiting = false;
            GotoNextPoint();   
        }
    }
}
