using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashDialog : MonoBehaviour
{

    public GameObject EnemyGenerator;
    public GameObject Score;
    public GameObject MainCharacter;
    public List<GameObject> Backgrounds;
    private bool _IsGameStarted;

    //private EnemyGeneratorScript enemyGeneratorScript;

    // Start is called before the first frame update
    void Start()
    {
        EnemyGenerator.SetActive(false);
        Score.SetActive(false);
        _IsGameStarted = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (!_IsGameStarted && Input.GetKeyDown(KeyCode.Return))
        {

            Text text = gameObject.GetComponent<Text>();
            text.text = "";

            EnemyGenerator.SetActive(true);
            EnemyGenerator.GetComponent<GenerateEnemies>().IsCoroutineExecuting = false;

            Score.SetActive(true);
            _IsGameStarted = true;
            MovementAlwaysRun movement = MainCharacter.GetComponent<MovementAlwaysRun>();
            movement.HorizontalSpeed = 20f;
            var highScoreCont = MainCharacter.GetComponent<HighScoreContainer>();
            highScoreCont.NewHighScore = false;
            highScoreCont.StartPosition = MainCharacter.transform.position.x;

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGameLayout();
        }
    }

    public void ShowGameOverMessage()
    {

        MovementAlwaysRun movement = MainCharacter.GetComponent<MovementAlwaysRun>();
        movement.HorizontalSpeed = 0f;
        MainCharacter.GetComponent<Rigidbody2D>().velocity = new Vector2();

        var highScoreCont = MainCharacter.GetComponent<HighScoreContainer>();

        if (highScoreCont.NewHighScore)
        {
            Text text = gameObject.GetComponent<Text>();
            text.text = "Game Over!"  + Environment.NewLine + "Congratulations! You beat the high score!" + Environment.NewLine + "Press Enter to Start!";
        }
        else
        {
            Text text = gameObject.GetComponent<Text>();
            text.text = "Game Over!" + Environment.NewLine + "Press Enter to Start!";
        }

        // Stop new enemy generation
        EnemyGenerator.SetActive(false);

        // Destroy all active enemies
        var gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].activeSelf)
            {
                Destroy(gameObjects[i]);
            }
        }

        _IsGameStarted = false;
        

        print(_IsGameStarted);
        print(Input.GetButton("Jump"));
    }

    private void ResetGameLayout()
    {
        ResetLayoutController rlc;

        for (int i = 0; i < Backgrounds.Count; i++)
        {
            rlc = Backgrounds[i].GetComponent<ResetLayoutController>();
            rlc.ResetLayout();
        }

        rlc = MainCharacter.GetComponent<ResetLayoutController>();
        rlc.ResetLayout();
    }
}
