﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavMenuController : MonoBehaviour
{
    public List <GameObject> buttons = new List<GameObject>();
    PlayerState ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
        int level = (int)ps.Level + 1;
        Debug.Log(level);
        buttons[level * 2 - 1].GetComponent<Button>().interactable = true;
        buttons[level * 2 - 2].GetComponent<Button>().interactable = true;

    }

    public void Merchant()
    {
        ps.LevelType = 0;
        ps.Level++;
        Debug.Log(ps.Level);
        Deactivate();

        ps.increment += 2;

        if (ps.increment >= buttons.Count - 1)
            return;

        Activate();
        LoadScene();
    }

    public void Enemy()
    {
        ps.LevelType = 1;
        ps.Level += 1;

        Deactivate();

        ps.increment += 2;

        if (ps.increment >= buttons.Count - 1)
            return;

        Activate();
        LoadScene();
    }

    void Activate()
    {
        int i = ps.increment;
        int j = ps.increment + 1;
        buttons[i].GetComponent<Button>().interactable = true;
        buttons[j].GetComponent<Button>().interactable = true;
    }

    void Deactivate()
    {
        int i = ps.increment;
        int j = ps.increment + 1;
        buttons[i].GetComponent<Button>().interactable = false;
        buttons[j].GetComponent<Button>().interactable = false;
    }

    void LoadScene()
    {
        ps.SavePlayer();
        SceneManager.LoadScene("Ship Interior 2");
    }
}