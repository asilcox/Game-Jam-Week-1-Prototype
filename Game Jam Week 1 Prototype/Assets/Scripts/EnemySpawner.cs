using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    private int maxEnemies = 16;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies != null && enemies.Length < maxEnemies)
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
