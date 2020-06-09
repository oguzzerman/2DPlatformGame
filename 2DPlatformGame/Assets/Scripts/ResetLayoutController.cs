using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLayoutController : MonoBehaviour
{
    public float DefaultXPosition;
    public float DefaultYPosition;
    public float DefaultZPosition;

    // Start is called before the first frame update
    void Start()
    {
        DefaultXPosition = gameObject.transform.position.x;
        DefaultYPosition = gameObject.transform.position.y;
        DefaultZPosition = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetLayout()
    {
        //gameObject.transform.position = new Vector3(DefaultXPosition, DefaultYPosition, DefaultZPosition);
    }

}
