using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSystem : MonoBehaviour
{
    private bool isNear = false;
    private PlayerState ps;
    private Animator anim;
    [SerializeField] private float repairCost = 25;
    [SerializeField] private GameObject key;
    private GameObject temp;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isNear && ps.SystemParts >= repairCost && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("SystemOn");
            ps.SystemParts -= repairCost;
            ps.RepairStatus += 25;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isNear = true;
            Vector3 pos = transform.position;
            pos.y += 1.5f; // hover above
            temp = Instantiate(key, pos, Quaternion.identity);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isNear = false;
            Destroy(temp);
        }
    }
}
