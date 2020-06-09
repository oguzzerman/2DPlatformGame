using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateScore : MonoBehaviour
{
    public GameObject MainCharacter;
    private Text _Score;
    private HighScoreContainer highScoreCont;

    // Start is called before the first frame update
    void Start()
    {
        _Score = GetComponent<Text>();
        highScoreCont = MainCharacter.GetComponent<HighScoreContainer>();

    }

    // Update is called once per frame
    void Update()
    {
        highScoreCont.Score = MainCharacter.transform.position.x - highScoreCont.StartPosition;
        _Score.text = "Score: " + highScoreCont.Score.ToString("F2");

        if (highScoreCont.Score > highScoreCont.HighScore)
        {
            highScoreCont.NewHighScore = true;
            highScoreCont.HighScore = highScoreCont.Score;
        }
    }
}
