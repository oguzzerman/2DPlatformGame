using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateHighScore : MonoBehaviour
{
    public GameObject MainCharacter;
    private Text _HighScore;
    private HighScoreContainer highScoreCont;
    // Start is called before the first frame update
    void Start()
    {
        _HighScore = GetComponent<Text>();
        highScoreCont = MainCharacter.GetComponent<HighScoreContainer>();

    }

    // Update is called once per frame
    void Update()
    {
        _HighScore.text = "High Score: " + highScoreCont.HighScore.ToString("F1");
    }
}
