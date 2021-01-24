using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    private bool isNear = false;
    private PlayerState ps;
    [SerializeField] private float cost = 25;
    [SerializeField] private float quantityPartsSold = 25;
    [SerializeField] private GameObject key;
    private GameObject temp;

    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject merchantMenuUI;
    [SerializeField] private GameObject gameComponents;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (isNear && ps.Money >= cost && Input.GetKeyDown(KeyCode.E))
        {
            // Load Shop Scene
            ps.Money -= cost;
            ps.SystemParts += quantityPartsSold;
            ChangePrice();
        }
    }
    private void ChangePrice()
    {
        cost *= ps.Level;
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
