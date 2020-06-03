using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float speed;
    private bool _LeftArrowDown;
    private bool _RightArrowDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _LeftArrowDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _LeftArrowDown = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _RightArrowDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _RightArrowDown = false;
        }
    }
   

    void FixedUpdate()
    {
        if (_LeftArrowDown)
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        else if (_RightArrowDown)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
    }
}
