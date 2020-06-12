using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject MainCharacter;
    public GameObject ScoreManagerObject;
    public GameObject LowEagle;
    public GameObject HighEagle;

    public GameObject Opossum;
    public bool IsCoroutineExecuting = false;

    private ScoreManager _ScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        _ScoreManager = ScoreManagerObject.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

        float time = DetermineGenerationFrequency();
        StartCoroutine(GenerateEnemyAfterTime(time));
    }

    private float DetermineGenerationFrequency()
    {
        float time = Random.Range(1f, 2.5f);
        return time;
    }

    IEnumerator GenerateEnemyAfterTime(float time)
    {
        if (IsCoroutineExecuting)
        {
            yield break;
        }

        IsCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        GenerateRandomEnemies();

        IsCoroutineExecuting = false;
    }


    private void GenerateRandomEnemies()
    {

        var index = UnityEngine.Random.Range(0, 2);

        GameObject enemy;
        EnemyMove enemyMove;

        if (index == 0)
        {
            if (_ScoreManager.GameScore < 150)
            {
                enemy = Instantiate(HighEagle);
                enemyMove = enemy.GetComponent<EnemyMove>();
            }
            else
            {
                enemy = Instantiate(LowEagle);
                enemyMove = enemy.GetComponent<EnemyMove>();
            }
        }
        else
        {
            enemy = Instantiate(Opossum);
            enemyMove = enemy.GetComponent<EnemyMove>();
        }

        enemy.SetActive(true);
        enemy.transform.position = new Vector3(gameObject.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
    }
}
