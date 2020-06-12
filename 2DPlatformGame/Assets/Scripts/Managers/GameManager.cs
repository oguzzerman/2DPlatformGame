using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject EnemyGeneratorObject; // Burayı yeniden isimlendirebiliriz
    public GameObject ScoreManagerObject; // Burayı yeniden isimlendirebiliriz
    public GameObject Score;
    public GameObject GameDialog;
    public GameObject MainCharacter;
    public List<GameObject> Backgrounds;
    private bool _IsGameStarted;
    private bool _IsFirstTime;
    private float _Speed;

    private ScoreManager _ScoreManager;

    //private EnemyGeneratorScript enemyGeneratorScript;

    // Start is called before the first frame update
    void Start()
    {
        _ScoreManager = ScoreManagerObject.GetComponent<ScoreManager>();

        EnemyGeneratorObject.SetActive(false);
        Score.SetActive(false);
        _IsGameStarted = false;
        _IsFirstTime = true;
    }


    // Update is called once per frame
    void Update()
    {

        if (_IsGameStarted)
        {
            _Speed = 20 + Math.Min(_ScoreManager.GameScore / 20f, 10f);
            AlwaysRun movement = MainCharacter.GetComponent<AlwaysRun>();
            movement.HorizontalSpeed = _Speed;
        }

        if (!_IsGameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            if (_IsFirstTime)
            {
                StartCoroutine(ShowMessage("Press Space to Jump." + Environment.NewLine + "Press Down Arrow to Crouch.", 3));
            }
            else
            {
                Text text = GameDialog.GetComponent<Text>();
                text.text = "";
            }
            _Speed = 20f;

            EnemyGeneratorObject.SetActive(true);
            EnemyGeneratorObject.GetComponent<EnemyGenerator>().IsCoroutineExecuting = false;

            Score.SetActive(true);
            _IsGameStarted = true;
            AlwaysRun movement = MainCharacter.GetComponent<AlwaysRun>();
            movement.HorizontalSpeed = _Speed;
            _ScoreManager.ResetGameStats();
            _ScoreManager.StartPosition = MainCharacter.transform.position.x;

            _IsFirstTime = false;
        }
    }

    public void FinishGame()
    {
        SoundManager.PlaySound("GameOver");
        AlwaysRun movement = MainCharacter.GetComponent<AlwaysRun>();
        movement.HorizontalSpeed = 0f;
        MainCharacter.GetComponent<Rigidbody2D>().velocity = new Vector2();

        Text text = GameDialog.GetComponent<Text>();

        text.enabled = true;

        if (_ScoreManager.NewHighScore)
        {
            text.text = "Game Over!" + Environment.NewLine + "Congratulations! You beat the high score!" + Environment.NewLine + "Press Enter to Start!";
        }
        else
        {
            text.text = "Game Over!" + Environment.NewLine + "Press Enter to Start!";
        }

        // Stop new enemy generation
        EnemyGeneratorObject.SetActive(false);

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
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        Text text = GameDialog.GetComponent<Text>();
        text.text = message;
        text.enabled = true;
        yield return new WaitForSeconds(delay);
        text.enabled = false;
    }
}
