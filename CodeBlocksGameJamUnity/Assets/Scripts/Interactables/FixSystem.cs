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
    private float health = 0;
    private Transform bar;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        anim = GetComponent<Animator>();
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.name == "Bar")
                bar = child;
        }
        bar.localScale = new Vector3(1f, 0f, 1f);
    }

    private void Update()
    {
        if (isNear && ps.SystemParts >= repairCost && Input.GetKeyDown(KeyCode.E) && health <= 0)
        {
            anim.SetBool("SystemOn", true);
            ps.SystemParts -= repairCost;
            ps.RepairStatus += 25f;
            SFXManager.instance.PlaySystemActivate();
            bar.localScale = new Vector3(1f, 1f, 1f);
            health = 100f;
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

        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Bullet") && health > 0)
        {
            health -= 5f;
            bar.localScale = new Vector3(1f, health/100f, 1f);
            if (health <= 0)
            {
                anim.SetBool("SystemOn", false);
                if (ps.RepairStatus > 0)
                {
                    ps.RepairStatus -= 25f;
                }
                
            }
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
