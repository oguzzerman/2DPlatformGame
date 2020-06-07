using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateScore : MonoBehaviour
{
    public GameObject MainCharacter;
    private Text _Score;
    // Start is called before the first frame update
    void Start()
    {
        _Score = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        _Score.text = "Score: " + MainCharacter.transform.position.x.ToString("F1");
    }
}
