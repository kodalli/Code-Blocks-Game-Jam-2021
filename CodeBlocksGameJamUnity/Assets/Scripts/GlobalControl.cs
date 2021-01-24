using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    // game variables
    public float HP;
    public float Money;
    public float RepairStatus;
    public float Level;

    private void Awake()
    {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

}
