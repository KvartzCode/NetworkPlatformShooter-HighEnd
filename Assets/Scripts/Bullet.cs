using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField]
    public int Damage { get; private set; } = 25;
    [field: SerializeField]
    public float DestroyDelay { get; private set; } = 3;


    void Start()
    {
        Destroy(gameObject, DestroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.root.GetComponent<PlayerController>().Damage(Damage);
        }

        Destroy(gameObject);
    }
}
