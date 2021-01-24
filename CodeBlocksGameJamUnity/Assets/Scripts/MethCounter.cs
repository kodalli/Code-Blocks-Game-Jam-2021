using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MethCounter : MonoBehaviour
{
    private TextMeshProUGUI methCounter;
    public LevelManager lm;

    private void Start()
    {
        methCounter = gameObject.GetComponent<TextMeshProUGUI>();
    }
    private void FixedUpdate()
    {
        methCounter.text = "Meth: ";
        methCounter.text = "Meth: ";
    }
}
