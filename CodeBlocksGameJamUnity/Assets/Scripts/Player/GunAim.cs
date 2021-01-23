using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    private Vector3 cursor;
    private Transform playerTransform;
    private Vector3 target;
    [SerializeField] private float gunRadius = 0.1f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void LateUpdate()
    {

        target = cursor - playerTransform.position;
        target.z = 0;
        transform.position = playerTransform.position + target.normalized*gunRadius;
        Vector2 dir = cursor - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (cursor.x < 0)
            transform.Rotate(180f, 0f, 0f);
    }



}
