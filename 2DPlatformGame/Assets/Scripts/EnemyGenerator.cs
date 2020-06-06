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
        StartCoroutine(ExecuteAfterTime(2));
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        print("girdi");
        if (isCoroutineExecuting)
        {
            print("break etti");
            yield break;
        }

        isCoroutineExecuting = true;
        print("break etmedi");

        yield return new WaitForSeconds(time);
        print("çağırdı");

        GenerateRandomEnemies();

        isCoroutineExecuting = false;
    }


    private void GenerateRandomEnemies()
    {
        var rnd = Random.Range(0, Enemies.Count);
        print(rnd);
        GameObject enemy = Instantiate(Enemies[rnd]);
        enemy.SetActive(true);
        enemy.transform.position = new Vector3(gameObject.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
    }
}
