using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevelManager : MonoBehaviour
{
    public static MapLevelManager instance;

    public Transform shipSpawnPoint;
    public GameObject shipPrefab;

    private void Awake()
    {
        instance = this;
        Instantiate(shipPrefab, shipSpawnPoint.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
