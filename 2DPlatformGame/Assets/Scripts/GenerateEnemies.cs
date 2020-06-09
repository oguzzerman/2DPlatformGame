using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject MainCharacter;
    public List<GameObject> Enemies;
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
        float time = 2;

        var highScoreCont = MainCharacter.GetComponent<HighScoreContainer>();
        time = 2 - Math.Min(highScoreCont.Score / 200f, 1.5f); 

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
        var index = UnityEngine.Random.Range(0, Enemies.Count);
        GameObject enemy = Instantiate(Enemies[index]);

        enemy.SetActive(true);
        enemy.transform.position = new Vector3(gameObject.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
    }
}
