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
    public GameObject spawner;

    private void Awake()
    {
        instance = this;
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        
    }

    private void FixedUpdate()
    {
        if (ps.LevelType == 0)
        {
            door.SetActive(false);
            for (int i = 0; i < spawner.transform.childCount; i++)
            {
                var child = spawner.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }
        }
        else if(ps.LevelType == 1 && ps.Spawn)
        {
            door.SetActive(true);
            for (int i = 0; i < spawner.transform.childCount; i++)
            {
                var child = spawner.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(true);
            }
        } else
        {
            door.SetActive(true);
            for (int i = 0; i < spawner.transform.childCount; i++)
            {
                var child = spawner.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(false);
            }
        }
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
