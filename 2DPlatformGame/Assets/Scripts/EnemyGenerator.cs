using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> Enemies;
    private bool isCoroutineExecuting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GenerateEnemyAfterTime(2));
    }


    IEnumerator GenerateEnemyAfterTime(float time)
    {
        if (isCoroutineExecuting)
        {
            yield break;
        }

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        GenerateRandomEnemies();

        isCoroutineExecuting = false;
    }


    private void GenerateRandomEnemies()
    {
        var index = Random.Range(0, Enemies.Count);
        GameObject enemy = Instantiate(Enemies[index]);
        enemy.SetActive(true);
        enemy.transform.position = new Vector3(gameObject.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
    }
}
