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
        float score = MainCharacter.transform.position.x;
        _Score.text = "Score: " + score.ToString("F1");

        if (score > highScoreCont.HighScore)
        {
            highScoreCont.NewHighScore = true;
            highScoreCont.HighScore = score;
        }
    }
}
