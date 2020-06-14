using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject EnemyGeneratorObject;
    public GameObject ScoreManagerObject;
    public GameObject Score;
    public GameObject GameDialog;
    public GameObject MainCharacter;
    public List<GameObject> Backgrounds;
    private bool _IsGameStarted;
    private bool _IsFirstTime;
    private float _Speed;

    private ScoreManager _ScoreManager;
    public Animator _AnimatorLeo;
    private bool IsCoroutineExecuting = false;

    //private EnemyGeneratorScript enemyGeneratorScript;

    // Start is called before the first frame update
    void Start()
    {
        _ScoreManager = ScoreManagerObject.GetComponent<ScoreManager>();
        _AnimatorLeo = MainCharacter.GetComponent<Animator>();
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
            _Speed = 200 + Math.Min(_ScoreManager.GameScore / 5f, 100f);
            CharacterController movement = MainCharacter.GetComponent<CharacterController>();
            movement.HorizontalSpeed = _Speed;
        }

        if (!_IsGameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            if (_IsFirstTime)
            {
                StartCoroutine(ShowMessage("Press Space or Up Arrow to Jump." + Environment.NewLine + "Press Left Shift or Down Arrow to Crouch.", 3));
            }
            else
            {
                Text text = GameDialog.GetComponent<Text>();
                text.text = "";
            }
            _Speed = 200f;

            EnemyGeneratorObject.SetActive(true);
            EnemyGeneratorObject.GetComponent<EnemyGenerator>().IsCoroutineExecuting = false;

            Score.SetActive(true);
            _IsGameStarted = true;
            CharacterController movement = MainCharacter.GetComponent<CharacterController>();
            movement.HorizontalSpeed = _Speed;
            _ScoreManager.ResetGameStats();
            _ScoreManager.StartPosition = MainCharacter.transform.position.x;

            _IsFirstTime = false;
        }
    }

    public void FinishGame()
    {
        SoundManager.PlaySound("GameOver");
        CharacterController movement = MainCharacter.GetComponent<CharacterController>();
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

        _AnimatorLeo.SetBool("GameOver", true);
        StartCoroutine(EndGameOverAnimation(1f));

    }

    IEnumerator EndGameOverAnimation(float time)
    {
        if (IsCoroutineExecuting)
        {
            yield break;
        }

        IsCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        _AnimatorLeo.SetBool("GameOver", false);

        IsCoroutineExecuting = false;
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
