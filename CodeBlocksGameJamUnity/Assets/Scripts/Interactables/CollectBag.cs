using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBag : MonoBehaviour
{
    PlayerState ps;
    private void Start()
    {
        SFXManager.instance.PlayPop();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ps.Money += 1f;
            Destroy(gameObject);
            //Debug.Log(ps.Money);
        }
    }
}
