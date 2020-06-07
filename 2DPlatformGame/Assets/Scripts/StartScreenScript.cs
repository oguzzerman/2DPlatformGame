using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScript : MonoBehaviour
{

    private GameObject EnemyGenerator;
    //private EnemyGeneratorScript enemyGeneratorScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            Destroy(gameObject);
            //EnemyGenerator.GetComponent<EnemyGeneratorScript>.SetActive(true);
        }

    }
}
