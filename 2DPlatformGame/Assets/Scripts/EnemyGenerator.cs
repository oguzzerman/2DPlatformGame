using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> Enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerateRandomEnemies();

    }

    private IEnumerator GenerateRandomEnemies()
    {
        yield return new WaitForSeconds(5);
        var rnd = Random.Range(0, Enemies.Count);
        print(rnd);
        GameObject enemy = Instantiate(Enemies[rnd]);
        enemy.active = true;

        
    }
}
