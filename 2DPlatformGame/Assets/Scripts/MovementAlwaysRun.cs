using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAlwaysRun : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float HorizontalSpeed = 40f;
    private bool _Jump = false;
    private bool _Crouch = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            _Jump = true;
            animator.SetBool("Jumping", true);
        }


        if (Input.GetButton("Crouch"))
        {
            _Crouch = true;

        }
        else// if (Input.GetButtonUp("Crouch"))
        {
            _Crouch = false;
        }


        animator.SetBool("Crouching", _Crouch);
    }

    public void OnLanding()
    {
        print("landed");
        animator.SetBool("Jumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(Time.fixedDeltaTime * HorizontalSpeed, _Crouch, _Jump);
        animator.SetFloat("Speed", Mathf.Abs(HorizontalSpeed));
        _Jump = false;
    }
}
