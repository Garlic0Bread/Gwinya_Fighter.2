using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private float followSpeed;
    [SerializeField] private Collider2D colliderToIgnore;
    [SerializeField] private GameObject explosionPrefab; 
    private GameObject player;
    private GameObject enemy;
    public bool canDamageEnemy;

    private void Start()
    {
        colliderToIgnore = gameObject.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("PlayerFeet");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        if (this.gameObject.CompareTag("EnemyBullet"))
        {
            Vector3 targetPos = player.transform.position;
            float speed = followSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPos, speed);
        }
        if (this.gameObject.CompareTag("PharaBullet"))
        {
            Vector3 targetPos = enemy.transform.position;
            float speed = followSpeed * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetPos, speed);
        }
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            if (this.gameObject.CompareTag("EnemyBullet") && collider.CompareTag("Enemy"))
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), colliderToIgnore);
            }
            else if (this.gameObject.CompareTag("EnemyBullet") && collider.CompareTag("Player"))
            {
                Health health = collider.GetComponent<Health>();
                health.Damage(damage);
                DestroyBullet();
            }
            else if (this.gameObject.CompareTag("PlayerBullet") && collider.CompareTag("Enemy"))
            {
                Health health = collider.GetComponent<Health>();
                health.Damage(damage);
                DestroyBullet();
            }
            
            else if (this.gameObject.CompareTag("PharaBullet") && collider.CompareTag("Enemy"))
            {
                Health health = collider.GetComponent<Health>();
                health.Damage(damage);
                DestroyBullet();
            }
        }
    }

    private void DestroyBullet()
    {
        
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
