using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float HP;
    public float Money;
    public float RepairStatus;
    public float Level;

    // Load data from GlobalControl
    private void Start()
    {
        HP = GlobalControl.Instance.HP;
        Money = GlobalControl.Instance.Money;
        RepairStatus = GlobalControl.Instance.RepairStatus;
        Level = GlobalControl.Instance.Level;

    }

    public void SavePlayer()
    {
        GlobalControl.Instance.HP = HP;
        GlobalControl.Instance.Money = Money;
        GlobalControl.Instance.RepairStatus = RepairStatus;
        GlobalControl.Instance.Level = Level;
    }
}
