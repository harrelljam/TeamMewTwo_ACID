using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController _cont;
    private Rigidbody _rb;
    private Animator _anim;
    private Transform _cam;

    public float WalkSpeed;
    public float RunSpeed;
    public float CrouchSpeed;
    //public float JumpForce;
    //public LayerMask Ground;
    //public float GroundDist;

    void Start()
    {
        _cont = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
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
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = _cam.TransformDirection(moveVector);
        _cont.SimpleMove(moveVector * Speed);

    }
    //bool Grounded()
    //{
    //    return Physics.Raycast(transform.position, Vector3.down, GroundDist, Ground);
    //}
}
