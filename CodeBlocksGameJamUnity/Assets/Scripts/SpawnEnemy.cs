using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    float nextSpawn = 0f;
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField][Range(0f, 1f)] private float spawnProbability = 0.3f;
    [SerializeField] private float timeToNextSpawn = 10f;
    private bool levelTimeUp = false;


    void FixedUpdate()
    {
        if (!levelTimeUp && Time.time > nextSpawn)
        {
            nextSpawn = Time.time + timeToNextSpawn;
            foreach (GameObject g in spawnPoints)
                if (Random.value < spawnProbability)
                    Instantiate(enemies[Random.Range(0, enemies.Length)], g.transform.position, Quaternion.identity);   
        } else if (Time.time > LevelManager.instance.levelTimeLength)
        {
            Debug.Log("");
            levelTimeUp = true;
            LevelManager.instance.ToggleSpawner(false);
        }
    }

}
