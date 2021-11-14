using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject MainCharacter;
    public GameObject HighScoreObj;
    private Text _Score;
    private HighScoreController highScoreCont;

    public float HighScore;
    public float StartPosition;
    private float _GameScore;
    private bool _NewHighScore;

    public float GameScore { get => _GameScore; }
    public bool NewHighScore { get => _NewHighScore; }

    // Start is called before the first frame update
    void Start()
    {
        _Score = GetComponent<Text>();
        highScoreCont = HighScoreObj.GetComponent<HighScoreController>();

    }

    // Update is called once per frame
    void Update()
    {
        _GameScore = MainCharacter.transform.position.x - StartPosition;
        _Score.text = "Score: " + GameScore.ToString("F2");

        if (GameScore > HighScore)
        {
            _NewHighScore = true;
            HighScore = GameScore;
            highScoreCont.UpdateHighScore(HighScore);
        }
    }

    public void ResetGameStats()
    {
        _GameScore = 0;
        _NewHighScore = false;
    }
}
