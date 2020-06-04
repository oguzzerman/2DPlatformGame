using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public CharacterController2D controller;
    public float HorizontalSpeed = 40f;
    private float _HorizontalMove;
    private bool _Jump = false;
    private bool _Crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _HorizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _Jump = true;
        }

        if (Input.GetButton("Crouch"))
        {
            _Crouch = true;
        }
        else// if (Input.GetButtonUp("Crouch"))
        {
            _Crouch = false;
        }
    }
   

    void FixedUpdate()
    {
        controller.Move(_HorizontalMove* Time.fixedDeltaTime * HorizontalSpeed, _Crouch, _Jump);
        _Jump = false;
        print(_Crouch);
    }
}
