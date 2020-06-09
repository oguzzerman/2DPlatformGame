using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementAlwaysRun : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float HorizontalSpeed = 40f;
    public bool Jumping = false;

    private bool _Jump = false;
    private bool _Crouch = false;
    private BoxCollider2D boxCollider;
    private float ColliderSizeY;

    public UnityEvent GameOverEvent;

    private void Awake()
    {
        if (GameOverEvent == null)
            GameOverEvent = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        ColliderSizeY = boxCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            _Jump = true;
            Jumping = true;
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
        Jumping = false;
        animator.SetBool("Jumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(Time.fixedDeltaTime * HorizontalSpeed, _Crouch, _Jump);
        animator.SetFloat("Speed", Mathf.Abs(HorizontalSpeed));
        _Jump = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            print(other.gameObject.name);
            GameOverEvent.Invoke();
        }
    }
}
