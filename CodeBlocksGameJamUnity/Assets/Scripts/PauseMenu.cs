using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameComponents;
    [SerializeField] private bool isPaused;

    private PlayerState ps;
    private void Start()
    {
        ps = LevelManager.instance.ps;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        pauseMenuUI.SetActive(true);
        gameComponents.SetActive(false);
    }
    void deactivateMenu()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        gameComponents.SetActive(true);
    }
}
