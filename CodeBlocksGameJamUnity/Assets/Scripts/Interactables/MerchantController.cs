using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    private bool isNear = false;
    private PlayerState ps;
    [SerializeField] private float cost = 25;
    [SerializeField] private float quantityPartsSold = 25;
    [SerializeField] private GameObject key;
    private GameObject temp;
    public GameObject pauseMenuUI, gameComponents;
    private bool isPause = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        //if (isNear && ps.Money >= cost && Input.GetKeyDown(KeyCode.E))
        //{
        //    // Load Shop Scene
        //    ps.Money -= cost;
        //    ps.SystemParts += quantityPartsSold;
        //    ChangePrice();
        //}

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

    private void ChangePrice()
    {
        cost *= ps.Level;
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
        pauseMenuUI.SetActive(true);
        gameComponents.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Shooting>().enabled = false;
    }
    void deactivateMenu()
    {
        pauseMenuUI.SetActive(false);
        gameComponents.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Shooting>().enabled = true;

    }
}
