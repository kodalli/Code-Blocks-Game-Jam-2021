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
}
