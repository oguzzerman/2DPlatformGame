using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour
{
    public float Speed;
    public float VerticalMoveSpeed;
    public float VerticalMoveRange;

    private float DefaultYPosition;


    // Start is called before the first frame update
    void Start()
    {
        DefaultYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        var rigidbody2D = GetComponent<Rigidbody2D>();

        if (transform.position.y > DefaultYPosition + VerticalMoveRange)
        {
            rigidbody2D.velocity = new Vector2(Speed, -VerticalMoveSpeed);
        }
        else if (transform.position.y <= DefaultYPosition)
        {
            rigidbody2D.velocity = new Vector2(Speed, VerticalMoveSpeed);
        }
    }
}
