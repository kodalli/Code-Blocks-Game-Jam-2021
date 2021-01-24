using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowderCount : MonoBehaviour
{

    [SerializeField] private int index = 0;

    private TextMeshProUGUI tmp;
    private string[] output = new string[4];

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
        output[3] = "Repair Status: " + ps.RepairStatus.ToString();
        tmp.text = output[index];
    }
}
