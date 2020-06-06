using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = new Vector2(Speed, 0);
    }
}
