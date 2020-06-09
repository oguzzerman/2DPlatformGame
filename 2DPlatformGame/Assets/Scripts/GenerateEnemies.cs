using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public List<GameObject> Enemies;

    public bool IsCoroutineExecuting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GenerateEnemyAfterTime(1));
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
        var index = Random.Range(0, Enemies.Count);
        GameObject enemy = Instantiate(Enemies[index]);

        enemy.SetActive(true);
        enemy.transform.position = new Vector3(gameObject.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
    }
}
