using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private int index = 0;

    private TextMeshProUGUI tmp;
    private string[] output = new string[3];

    PlayerState ps;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        output[0] = "Buy 10 Ship Parts MP" + (10 * ps.Level).ToString();
        output[1] = "Buy 15 Ship Parts MP" + (15 * ps.Level).ToString();
        output[2] = "Buy 25 Ship Parts MP" + (25 * ps.Level).ToString();
        tmp.text = output[index];
    }
}
