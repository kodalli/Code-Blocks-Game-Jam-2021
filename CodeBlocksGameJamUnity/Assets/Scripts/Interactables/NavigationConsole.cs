using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationConsole : MonoBehaviour
{
    private bool isNear = false;
    [SerializeField] private GameObject key;
    private GameObject temp;
    private PlayerState ps;
    private GameObject player;
    public GameObject navMenu, gameComponents;
    private bool isPause = false;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E) && !isPause)
        {
            ps.SavePlayer();
            isPause = true;
            activateMenu();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPause)
        {
            ps.SavePlayer();
            isPause = false;
            deactivateMenu();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isNear = true;
            Vector3 pos = transform.position;
            pos.y += 1.5f; // hover above
            temp = Instantiate(key, pos, Quaternion.identity);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isNear = false;
            Destroy(temp);
        }
    }

    void activateMenu()
    {
        navMenu.SetActive(true);
        gameComponents.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Shooting>().enabled = false;
    }
    void deactivateMenu()
    {
        navMenu.SetActive(false);
        gameComponents.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Shooting>().enabled = true;

    }
}
