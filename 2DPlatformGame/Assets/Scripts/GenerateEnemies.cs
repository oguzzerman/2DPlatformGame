using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject MainCharacter;
    public GameObject Eagle;
    public GameObject SecondEagle;
    public GameObject ThirdEagle;
    public GameObject Opossum;
    public bool IsCoroutineExecuting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float time = DetermineGenerationFrequency();
        StartCoroutine(GenerateEnemyAfterTime(time));
    }

    private float DetermineGenerationFrequency()
    {
        //float time = 2;

        //var highScoreCont = MainCharacter.GetComponent<HighScoreContainer>();
        //time = 2 - Math.Min(highScoreCont.Score / 200f, 1.5f); 

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
        var highScoreCont = MainCharacter.GetComponent<HighScoreContainer>();

        var index = UnityEngine.Random.Range(0, 2);

        GameObject enemy;
        EnemyMove enemyMove;
        if (index == 0)
        {
            enemy = Instantiate(Eagle);
            enemyMove = enemy.GetComponent<EnemyMove>();
            enemyMove.EnemyType = eEnemyType.Eagle;
        }
        else
        {
            enemy = Instantiate(Opossum);
            enemyMove = enemy.GetComponent<EnemyMove>();
            enemyMove.EnemyType = eEnemyType.Opossum;
        }


        if (enemyMove.EnemyType == eEnemyType.Eagle)
        {
            if (highScoreCont.Score > 200)
            {

                GameObject secondEagle = Instantiate(SecondEagle);
                secondEagle.SetActive(true);
                secondEagle.transform.position = new Vector3(gameObject.transform.position.x, secondEagle.transform.position.y, secondEagle.transform.position.z);

                if (highScoreCont.Score > 400)
                {
                    enemyMove.VerticalMove = true;
                    secondEagle.GetComponent<EnemyMove>().VerticalMove = true;

                    GameObject thirdEagle = Instantiate(ThirdEagle);
                    thirdEagle.GetComponent<EnemyMove>().VerticalMove = true;
                    thirdEagle.SetActive(true);
                    thirdEagle.transform.position = new Vector3(gameObject.transform.position.x, thirdEagle.transform.position.y, thirdEagle.transform.position.z);
                }
                else if (highScoreCont.Score > 300)
                {
                    enemyMove.VerticalMove = true;
                    secondEagle.GetComponent<EnemyMove>().VerticalMove = true;
                }
            }
            else if (highScoreCont.Score > 100)
            {
                enemyMove.VerticalMove = true;
            }
            else
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x, -2f, enemy.transform.position.z);
            }
        }

        enemy.SetActive(true);
        enemy.transform.position = new Vector3(gameObject.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
    }
}
