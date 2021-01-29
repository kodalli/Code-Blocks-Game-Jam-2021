using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private int index = 0;

    private TextMeshProUGUI tmp;
    private string[] output = new string[3];
    [SerializeField] private bool scaleCostWithLevel = false;
    [SerializeField] private List<int> itemCost = new List<int> { 10, 15, 25 };

    PlayerState ps;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        ps = LevelManager.instance.ps;
        if (scaleCostWithLevel)
        {
            output[0] = "Buy 10 Ship Parts MP" + (itemCost[0] * ps.Level).ToString();
            output[1] = "Buy 15 Ship Parts MP" + (itemCost[1] * ps.Level).ToString();
            output[2] = "Buy 25 Ship Parts MP" + (itemCost[2] * ps.Level).ToString();
        }
        else
        {
            output[0] = "Buy 10 Ship Parts MP" + (itemCost[0]).ToString();
            output[1] = "Buy 15 Ship Parts MP" + (itemCost[1]).ToString();
            output[2] = "Buy 25 Ship Parts MP" + (itemCost[2]).ToString();
        }
        tmp.text = output[index];

    }

}
