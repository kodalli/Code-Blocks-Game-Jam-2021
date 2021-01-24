using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform respawnPoint;
    public GameObject playerPrefab;

    private PlayerState ps;

    private void Awake()
    {
        instance = this;
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ps.SavePlayer();
            SceneManager.LoadScene("Start Menu");
        }
        if (Input.GetKeyDown(KeyCode.Space))
            ps.HP -= 10;
    }

    public void Respawn()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPoint.position;
        ps.HP = 100f;
    }

    // Call PlayerState.SavePlayer() each time a scene change happens to save information globally
}
