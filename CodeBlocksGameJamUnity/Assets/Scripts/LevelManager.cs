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
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    ps.SavePlayer();
        //    SceneManager.LoadScene("Start Menu");
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //    ps.SystemParts += 10;
        if (GlobalControl.Instance.LevelType == 0)
            door.SetActive(false);
        else
            door.SetActive(true);
    }

    public void Respawn()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPoint.position;
        ps.HP = 100f;
    }
    public void toMainMenu()
    {
        ps.SavePlayer();
        SceneManager.LoadScene("Start Menu");
    }

    // Call PlayerState.SavePlayer() each time a scene change happens to save information globally
}
