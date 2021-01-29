using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private PlayerState ps;

    private void Start()
    {
        ps = LevelManager.instance.ps;
    }
    public void Button1()
    {
        if (ps.Money >= 10)
        {
            ps.Money -= 10;
            ps.SystemParts += 10;
            ps.SavePlayer();
            SFXManager.instance.PlayPurchase();
        } 
        else
        {
            SFXManager.instance.PlayPurchaseFail();
        }
    }

    public void Button2()
    {
        if (ps.Money >= 15)
        {
            ps.Money -= 15;
            ps.SystemParts += 15;
            ps.SavePlayer();
            SFXManager.instance.PlayPurchase();
        }
        else
        {
            SFXManager.instance.PlayPurchaseFail();
        }
    }

    public void Button3()
    {
        if (ps.Money >= 25)
        {
            ps.Money -= 25;
            ps.SystemParts += 25;
            ps.SavePlayer();
            SFXManager.instance.PlayPurchase();
        }
        else
        {
            SFXManager.instance.PlayPurchaseFail();
        }
    }
}
