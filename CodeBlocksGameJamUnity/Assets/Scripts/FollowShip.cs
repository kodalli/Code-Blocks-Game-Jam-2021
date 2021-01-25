using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour
{
    public Transform Ship;
    public float smooth = 5f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Ship.position, Time.deltaTime * smooth);
    }

}
