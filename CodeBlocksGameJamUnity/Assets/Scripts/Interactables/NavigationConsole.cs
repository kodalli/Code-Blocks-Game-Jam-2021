using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationConsole : MonoBehaviour
{
    private bool isNear = false;
    [SerializeField] private GameObject key;
    private GameObject temp;
    //private PlayerState ps;

    private void Start()
    {
        //ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Navigation Console Clicked");
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
