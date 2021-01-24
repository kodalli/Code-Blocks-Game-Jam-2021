using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantMenu : MonoBehaviour
{
    [SerializeField] private GameObject merchantMenuUI;
    [SerializeField] private GameObject gameComponents;
    [SerializeField] private bool isPaused;

    private PlayerState ps;
    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ps.SavePlayer();
            isPaused = !isPaused;
        }
        if (isPaused)
            activateMenu();
        else
            deactivateMenu();
    }
    void activateMenu()
    {
        Time.timeScale = 0;
        merchantMenuUI.SetActive(true);
        gameComponents.SetActive(false);
    }
    void deactivateMenu()
    {
        Time.timeScale = 1;
        merchantMenuUI.SetActive(false);
        gameComponents.SetActive(true);
    }
}
