using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;

    public LevelManager lm;

    private void Start()
    {
        StartCoroutine(Death());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            return;
        Die();
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        Die();

    }

    void Die()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);

        lm.addMeth();
    }
}
