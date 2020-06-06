using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public List<GameObject> Backgrounds;
    //public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetCameraBoundingBox();
    }


    void GetCameraBoundingBox()
    {
        var cam = Camera.main;
        print("test");
    }
}
