using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum eJumpState
{
    Landed = 0,
    Rising = 1,
    Falling = 2
}

public class CharacterController : MonoBehaviour
{
    public Animator AnimatorLeo;
    public float HorizontalSpeed = 40f;
    public float JumpForce = 600f;
    [Range(0, 1)] public float CrouchSpeedFactor = 1f;
    public bool IsJumping = false;
    public UnityEvent GameOverEvent;
    public bool IsCoroutineExecuting = false;

    private bool _Crouch = false;
    private BoxCollider2D _BoxCollider;
    private Rigidbody2D _Rigidbody2D;
    private float _ColliderSizeY;
    private eJumpState _CurrentJumpState;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private void Awake()
    {
        if (GameOverEvent == null)
            GameOverEvent = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        _CurrentJumpState = eJumpState.Landed;
        _BoxCollider = GetComponent<BoxCollider2D>();
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _ColliderSizeY = _BoxCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSwipe();

        _CurrentJumpState = UpdateJumpsState();

        AnimatorLeo.SetBool("Crouching", _Crouch);

        // Landing check
        if (_CurrentJumpState == eJumpState.Landed)
        {
            IsJumping = false;
            AnimatorLeo.SetBool("Jumping", false);
            AnimatorLeo.SetBool("Falling", false);
        }
        else if (_CurrentJumpState == eJumpState.Falling)
        {
            AnimatorLeo.SetBool("Falling", true);
        }
    }

    void FixedUpdate()
    {
        Move();
        AnimatorLeo.SetFloat("Speed", Mathf.Abs(HorizontalSpeed));
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            GameOverEvent.Invoke();
        }
    }

    private void Move()
    {
        var speed = Time.fixedDeltaTime * HorizontalSpeed;

        if (_Crouch)
        {
            // Reduce the speed by the crouchSpeed factor
            speed *= CrouchSpeedFactor;
        }


        _Rigidbody2D.velocity = new Vector2(speed, _Rigidbody2D.velocity.y);
    }

    public void CheckSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            else if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0)
                {
                    if (_CurrentJumpState == eJumpState.Landed)
                    {
                        _CurrentJumpState = eJumpState.Rising;
                        SoundManager.PlaySound("Jump");
                        IsJumping = true;
                        AnimatorLeo.SetBool("Jumping", true);
                        AnimatorLeo.SetBool("Falling", false);

                        Jump();
                    }
                }
                else
                {
                    _Crouch = true;
                    _BoxCollider.size = new Vector2(_BoxCollider.size.x, _ColliderSizeY / 2);
                    StartCoroutine(GetUp(1f));
                }
            }
        }
    }

    IEnumerator GetUp(float time)
    {
        if (IsCoroutineExecuting)
        {
            yield break;
        }

        IsCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        _Crouch = false;
        _BoxCollider.size = new Vector2(_BoxCollider.size.x, _ColliderSizeY);

        IsCoroutineExecuting = false;
    }

    private eJumpState UpdateJumpsState()
    {
        eJumpState ret = eJumpState.Landed;

        if (_Rigidbody2D.velocity.y > 0.0000001)
        {
            ret = eJumpState.Rising;
        }
        else if (_Rigidbody2D.velocity.y < 0)
        {
            ret = eJumpState.Falling;
        }
        else if (_CurrentJumpState == eJumpState.Falling && Mathf.Abs(_Rigidbody2D.velocity.y) < 0.0000001)
        {
            ret = eJumpState.Landed;
        }

        return ret;
    }

    private void Jump()
    {
        _Rigidbody2D.velocity = new Vector2(0f, 0.0001f);
        _Rigidbody2D.AddForce(new Vector2(0f, JumpForce));

    }
}
