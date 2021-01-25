using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private PlayerState ps;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }
    public void Button1()
    {
        if (ps.Money >= ps.Level * 10)
        {
            ps.Money -= ps.Level * 10;
            ps.SystemParts += 10;
            ps.SavePlayer();
        }
    }

    public void Button2()
    {
        if (ps.Money >= ps.Level * 15)
        {
            ps.Money -= ps.Level * 15;
            ps.SystemParts += 15;
            ps.SavePlayer();
        }
    }

    public void Button3()
    {
        if (ps.Money >= ps.Level * 25)
        {
            ps.Money -= ps.Level * 25;
            ps.SystemParts += 25;
            ps.SavePlayer();
        }
    }
}
