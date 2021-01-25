using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float HP;
    public float Money;
    public float RepairStatus;
    public float Level;
    public float SystemParts;

    // Load data from GlobalControl
    private void Start()
    {
        HP = GlobalControl.Instance.HP;
        Money = GlobalControl.Instance.Money;
        RepairStatus = GlobalControl.Instance.RepairStatus;
        Level = GlobalControl.Instance.Level;
        SystemParts = GlobalControl.Instance.SystemParts;
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.HP = HP;
        GlobalControl.Instance.Money = Money;
        GlobalControl.Instance.RepairStatus = RepairStatus;
        GlobalControl.Instance.Level = Level;
        GlobalControl.Instance.SystemParts = SystemParts;
    }

    public void Button1()
    {
        if (Money >= Level*10)
        {
            Money -= Level * 10;
            SystemParts += 10;
            SavePlayer();
        }
    }

    public void Button2()
    {
        if (Money >= Level * 15)
        {
            Money -= Level * 15;
            SystemParts += 15;
            SavePlayer();
        }
    }

    public void Button3()
    {
        if (Money >= Level * 25)
        {
            Money -= Level * 25;
            SystemParts += 25;
            SavePlayer();
        }
    }
}
