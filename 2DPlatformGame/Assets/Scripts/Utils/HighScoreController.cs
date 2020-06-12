using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    private Text _HighScore;
    // Start is called before the first frame update
    void Start()
    {
        _HighScore = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHighScore(float HighScore)
    {
        _HighScore.text = "High Score: " + HighScore.ToString("F2");
    }
}
