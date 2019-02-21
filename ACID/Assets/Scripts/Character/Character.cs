using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character I;  
    public float WalkSpeed;
    public float RunSpeed;
    public float CrouchSpeed;
    public PauseMenu pauseMenu;

    public bool FootstepsOn;
    public AudioClip[] StepSounds;

    public bool hasKey = false;
    public GameObject messageCanvas;
    public TextMeshProUGUI messageText;
    private Coroutine messageCoroutine;
    
    private CharacterController _cont;
    private Rigidbody _rb;
    private Animator _anim;
    private AudioSource _aud;
    private Transform _cam;
    private bool _stepping = false;
    
    //public float JumpForce;
    //public LayerMask Ground;
    //public float GroundDist;

    void Awake()
    {
        I = this;
    }
    void Start()
    {
        _cont = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _aud = GetComponent<AudioSource>();
        _cam = GetComponentInChildren<Camera>().transform;
    }
    void Update()
    {
        Move();
        //Jump();
    }
    private void Move()
    {
        float Speed;
        //Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = RunSpeed;
            _anim.SetBool("Crouch", false);
        }
        //Crouching
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            Speed = CrouchSpeed;
            _anim.SetBool("Crouch", true);
        }
        //Walking
        else
        {
            Speed = WalkSpeed;
            _anim.SetBool("Crouch", false);
        }

        if (FootstepsOn && !_stepping && (Input.GetAxis("Horizontal") >= 0.1f || Input.GetAxis("Vertical") >= 0.1f))
        {
            _stepping = true;
            StartCoroutine(Steps(0.5f));
        }

        //check that the game isn't paused
        if (pauseMenu.paused)
        {
            Speed = 0f;
        }

        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = _cam.TransformDirection(moveVector);
        _cont.SimpleMove(moveVector * Speed * Time.deltaTime);

    }
    private IEnumerator Steps(float s)
    {
        _aud.clip = StepSounds[Random.Range(0, StepSounds.Length - 1)];
        _aud.Play();
        yield return new WaitForSeconds(s);
        _stepping = false;

    }

    public void DisplayMessage()
    {
        if(messageCoroutine != null)
            StopCoroutine(messageCoroutine);
        messageCoroutine = StartCoroutine(Message(3f));
    }

    public void DestroyMessage()
    {
        if(messageCoroutine != null)
            StopCoroutine(messageCoroutine);
        messageCanvas.SetActive(false);
    }
    private IEnumerator Message(float s)
    {
        messageCanvas.SetActive(true);
        yield return new WaitForSeconds(s);
        messageCanvas.SetActive(false);

    }
}
