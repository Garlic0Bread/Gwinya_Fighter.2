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

    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameObject player;

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
            if (gameObject.layer == 3 && Vector3.Distance(transform.position, closestPlayer.position) <= lockOnRange) //tokolishi layer
            {
                Swarm();
            }

            else if (gameObject.layer == 6 && Vector3.Distance(transform.position, closestPlayer.position) <= lockOnRange) //pinky pinky layer
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
            float distance = Vector3.Distance(transform.position, player.transform.position);

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
        if(collider.gameObject.tag == ("Player"))
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
            Destroy(gameObject);
        }
        if (collider.gameObject.tag == ("SafeArea"))
        {

        }
    }

    
}
