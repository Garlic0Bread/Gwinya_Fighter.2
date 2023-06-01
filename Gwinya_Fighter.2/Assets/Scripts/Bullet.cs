using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private Collider2D colliderToIgnore;

    private void Start()
    {
        GameObject playerCollider = GameObject.FindGameObjectWithTag("Player");
        colliderToIgnore = playerCollider.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Health>() != null)
        {
            if (collider.CompareTag("Player"))
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), colliderToIgnore);
            }
            else
            {
                Health health = collider.GetComponent<Health>();
                health.Damage(damage);
                Destroy(gameObject);

            }
        }
    }
}
