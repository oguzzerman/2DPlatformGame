using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnBecameInvisible()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();

        gameObject.transform.position += Vector3.right * 8 * 3;

    }
}
