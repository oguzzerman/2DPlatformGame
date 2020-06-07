using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float HorizontalSpeed = 40f;
    private float _HorizontalMove;
    private bool _Jump = false;
    private bool _Crouch = false;
    private BoxCollider2D boxCollider;
    private float ColliderSizeY;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        ColliderSizeY = boxCollider.size.y;
    }


    // Update is called once per frame
    void Update()
    {
        _HorizontalMove = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(_HorizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            _Jump = true;
            animator.SetBool("Jumping", true);
        }


        if (Input.GetButton("Crouch"))
        {
            _Crouch = true;
            boxCollider.size = new Vector2(boxCollider.size.x, ColliderSizeY / 2);
        }
        else// if (Input.GetButtonUp("Crouch"))
        {
            _Crouch = false;
            boxCollider.size = new Vector2(boxCollider.size.x, ColliderSizeY);
        }

        animator.SetBool("Crouching", _Crouch);
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(_HorizontalMove* Time.fixedDeltaTime * HorizontalSpeed, _Crouch, _Jump);
        _Jump = false;
    }
}
