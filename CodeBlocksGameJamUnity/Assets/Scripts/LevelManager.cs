using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform respawnPoint;
    public GameObject playerPrefab;
    public GameObject door;
    public PlayerState ps;
    public GameObject spawner;
    public readonly float levelTimeLength = 60f;

    private void Awake()
    {
        instance = this;
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        ps = GetComponent<PlayerState>();

    }

    private void Start()
    {
        if (ps.LevelType == 0)
        {
            // Activate Merchant Level
            door.SetActive(false);
            ToggleSpawner(false);
        }
        else
        {
            // Activate Enemy level
            door.SetActive(true);
            ToggleSpawner(true);
        }
    }

    public void Respawn()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPoint.position;
        ps.HP = 100f;
    }
    public void ToMainMenu()
    {
        ps.SavePlayer();
        SceneManager.LoadScene("Start Menu");
    }

    public void ToggleSpawner(bool flag)
    {
        spawner.SetActive(flag);
    }

}
