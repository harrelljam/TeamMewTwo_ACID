using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    //Static Instance, to access script from other scripts use "Character.I.<public variable/method>"
    public static Character I;  
    
    public float WalkSpeed;
    public float RunSpeed;
    public float CrouchSpeed;
    public bool hasKey = false;
    public bool FootstepsOn;
    public PauseMenu pauseMenu;
    public GameObject messageCanvas;
    public TextMeshProUGUI messageText;
    public AudioClip[] StepSounds;
    public Transform Camera;
    public GameObject Flightlight;
    
    private Coroutine messageCoroutine;
    private CharacterController _cont;
    private Animator _anim;
    private AudioSource _aud;
    private Transform _cam;
    private bool _stepping = false;

    void Awake()
    {
        I = this;     
    }
    void Start()
    {
        _cont = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _aud = GetComponent<AudioSource>();
        _cam = GetComponentInChildren<Camera>().transform;
        StartCoroutine(WaitDestroyMessage(3f, "Strange, the power went out... I should go down to the generator room"));
    }
    void FixedUpdate()
    {
        Move();
    }
    /// <summary>
    /// User input for movement, as well as step sounds.
    /// </summary>
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

        //Step sounds
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
    /// <summary>
    /// Enumerator for playing step sounds while walking/running/crouching
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private IEnumerator Steps(float s)
    {
        _aud.clip = StepSounds[Random.Range(0, StepSounds.Length - 1)];
        _aud.Play();
        yield return new WaitForSeconds(s);
        _stepping = false;

    }

    /// <summary>
    /// Displays the canvas, the messageText will become message.
    /// </summary>
    public void DisplayMessage(String message)
    {
        messageText.text = message;
        messageCanvas.SetActive(true);
    }
    
    /// <summary>
    /// Displays the canvas, the messageText will become message and will destroy message after time
    /// </summary>
    public void DisplayMessageTimed(String message, float time)
    {
        StartCoroutine(WaitDestroyMessage(time, message));
    }

    /// <summary>
    /// interrupts the 3 second message display
    /// </summary>
    public void DestroyMessage()
    {
        messageCanvas.SetActive(false);
    }

    /// <summary>
    /// called when player caught by AI
    /// </summary>
    /// <param name="deathSpot"></param>
    public void Death(Transform deathSpot)
    {
        Camera.gameObject.GetComponent<MouseLook>().enabled = false;
        Camera.parent = deathSpot;
        Camera.transform.localPosition = new Vector3(0,0,0);
        Camera.transform.localRotation = new Quaternion(0,0,0,0);
        StartCoroutine(WaitDie(2f));
    }
    
    
    private IEnumerator WaitDie(float s)
    {
        yield return new WaitForSeconds(s);
        SceneLoader.I.LoadScene(1);
    }
    
    private IEnumerator WaitDestroyMessage(float s, String text)
    {
        yield return new WaitForSeconds(1f);
        messageText.text = text;
        messageCanvas.SetActive(true);
        yield return new WaitForSeconds(s);
        if (messageText.text == text)
            messageCanvas.SetActive(false);
    }
}