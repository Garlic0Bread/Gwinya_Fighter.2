using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float fireRate = 0.5f; // Rate of fire (in seconds)
    [SerializeField] private float nextFireTime; // Time of the next allowed fire
    [SerializeField] private float lockOnRange = 10f; // Range within which player can be locked onto
    [SerializeField] private float bulletSpeed = 1.5f;

    [SerializeField] private GameObject first_Form;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject second_Form;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private BoxCollider2D boxCollider;
    
    private GameObject player;
    public bool canBeDamaged = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        if (player.Length > 0)
        {
            // Find the closest enemy
            Transform closestPlayer = GetClosestPlayer(player);

            // Check if the closest enemy is within lock-on range
            if (gameObject.layer == 3 && (transform.position - closestPlayer.position).sqrMagnitude <= lockOnRange * lockOnRange) //tokolishi layer
            {
                Swarm();
            }

            else if (gameObject.layer == 6 && (transform.position - closestPlayer.position).sqrMagnitude <= lockOnRange) //pinky pinky layer
            {
                
                if (Time.time >= nextFireTime)
                {
                    ShootPlayer();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }
    
    Transform GetClosestPlayer(GameObject[] players)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = (transform.position - player.transform.position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = player.transform;
            }
        }

        return closestEnemy;
    }

    private void ShootPlayer()
    {
        GameObject bullet = Instantiate(enemyBullet, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bulletSpawnPoint.up * bulletSpeed;
    }
    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void IncreaseSpeed(float increaseBy)
    {
        speed = speed + increaseBy;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("SafeArea"))
        {
            canBeDamaged = true;
            first_Form.SetActive(false);
            second_Form.SetActive(true);
        }
        if(collider.gameObject.CompareTag("PlayerBullet"))
        {
            if (canBeDamaged == true)
            {
                Health health = GetComponent<Health>();
                Bullet bullet = FindObjectOfType<Bullet>();

                Destroy(collider.gameObject);
                health.Damage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SafeArea"))
        {
            canBeDamaged = false;
            first_Form.SetActive(true);
            second_Form.SetActive(false);
        }
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (canBeDamaged == false)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.GetComponent<Collider2D>());
            }
        }
    }
}
