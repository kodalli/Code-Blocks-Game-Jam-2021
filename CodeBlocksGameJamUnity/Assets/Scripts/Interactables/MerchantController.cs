using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    private bool isNear = false;
    private PlayerState ps;
    [SerializeField] private GameObject key;
    private GameObject temp;
    public GameObject pauseMenuUI, gameComponents;
    private bool isPause = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = LevelManager.instance.ps;
    }

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E) && !isPause)
        {
            ps.SavePlayer();
            isPause = true;
            ActivateMenu();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPause)
        {
            ps.SavePlayer();
            isPause = false;
            DeactivateMenu();
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

    private void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        gameComponents.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Shooting>().enabled = false;
    }
    private void DeactivateMenu()
    {
        pauseMenuUI.SetActive(false);
        gameComponents.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Shooting>().enabled = true;

    }
}
