using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowderCount : MonoBehaviour
{

    [SerializeField] private int index = 0;

    private TextMeshProUGUI tmp;
    private string[] output = new string[6];
    private float timer = 60f;
    PlayerState ps;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        output[0] = "Magic Powder: " + ps.Money.ToString();
        output[1] = "Health: " + ps.HP.ToString();
        output[2] = "Level: " + ps.Level.ToString();
        output[3] = "Repair Status: " + ps.RepairStatus.ToString() + "%";
        output[4] = "System Parts: " + ps.SystemParts.ToString();
        output[5] = "Time Left: " + timer.ToString();
        tmp.text = output[index];

        timer = 60 - Time.timeSinceLevelLoad;
        if (timer <= 0)
        {
            ps.Spawn = false;
            foreach(var g in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(g);
            timer = 0f;
        }
    }


}
