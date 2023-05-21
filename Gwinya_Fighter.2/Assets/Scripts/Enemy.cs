using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 1.5f;

    [SerializeField] private float lockOnRange = 10f; // Range within which player can be locked onto

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
            Transform closestEnemy = GetClosestPlayer(player);

            // Check if the closest enemy is within lock-on range
            if (Vector3.Distance(transform.position, closestEnemy.position) <= lockOnRange)
            {
                Swarm();
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

    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void stopMoving()
    {
        transform.position = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
            Destroy(gameObject);
        }
    }
}
