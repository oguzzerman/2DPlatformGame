using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyType
{
    Eagle = 0,
    Opossum = 1
}

public class EnemyMove : MonoBehaviour
{

    public float Speed;
    private float DefaultYPosition;
    public eEnemyType EnemyType;
    public bool VerticalMove = false;

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

        if (EnemyType == eEnemyType.Eagle)
        {
            if (VerticalMove)
            {
                if (transform.position.y > DefaultYPosition + 1)
                {
                    rigidbody2D.velocity = new Vector2(Speed, -1f);
                }
                else if (transform.position.y <= DefaultYPosition)
                {
                    rigidbody2D.velocity = new Vector2(Speed, 1f);
                }
            }
            else
            {
                rigidbody2D.velocity = new Vector2(Speed, 0);
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(Speed, 0);
        }
    }
}
