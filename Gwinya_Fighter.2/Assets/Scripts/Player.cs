using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerOriginalSpeed;
    [SerializeField] private float bulletSpeed = 10f; // Speed of the bullet
    [SerializeField] private float lockOnRange = 10f; // Range within which enemies can be locked onto
    [SerializeField] private float fireRate = 0.5f; // Rate of fire (in seconds)
    [SerializeField] private float nextFireTime; // Time of the next allowed fire
    [SerializeField] private float playerSpeed;

    [SerializeField] private Joystick_Movement joystickMovement;
    [SerializeField] private Transform playerGun; // Reference to the player's gun transform

    [SerializeField] private GameObject enableShield;
    [SerializeField] private GameObject bulletPrefab; // Prefab of the bullet to be spawned
    [SerializeField] private GameObject phara1;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    public bool pharas_Active;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSpeed = playerOriginalSpeed;
        enableShield.SetActive(false);
        phara1.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinManger coin = FindObjectOfType<CoinManger>();
        if (collision.gameObject.CompareTag("Pharas") && coin.PlayerGwinyas > 4)
        {
            coin.PlayerGwinyas = coin.PlayerGwinyas - 5;
            collision.gameObject.SetActive(false);
            StartCoroutine(ActivatePharaGroup());
        }
        if (collision.gameObject.CompareTag("Shield"))
        {
            StartCoroutine(EnableShield());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ActivatePharaGroup()
    {
        phara1.SetActive(true);
        yield return new WaitForSeconds(2);
        pharas_Active = true;
    }
    
    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    IEnumerator tempSpeedBoost()
    {
        playerSpeed = playerSpeed * 2;
        yield return new WaitForSeconds(8);
        playerSpeed = playerOriginalSpeed;
    }

    IEnumerator EnableShield()
    {
        enableShield.SetActive(true);
        AbilityManager deathSound = FindObjectOfType<AbilityManager>();
        deathSound.shieldActivated();
        yield return new WaitForSeconds(10);
        enableShield.SetActive(false);
    }
    
    public void SpeedBoost()
    {
        StartCoroutine(tempSpeedBoost());
    }

    void FireBullet()
    {
        // Instantiate a bullet prefab at the player's gun position and rotation
        GameObject bullet = Instantiate(bulletPrefab, playerGun.position, Quaternion.identity);

        // Apply velocity to the bullet in the forward direction of the gun
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = playerGun.forward * bulletSpeed;
    }

    void Update()
    {
        // Find all enemy game objects within lock-on range
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Check if there are any enemies in range
        if (enemies.Length > 0)
        {
            // Find the closest enemy
            Transform closestEnemy = GetClosestEnemy(enemies);

            // Check if the closest enemy is within lock-on range
            if (Vector3.Distance(transform.position, closestEnemy.position) <= lockOnRange)
            {
                // Rotate the player's gun to face the closest enemy
                Vector3 targetDirection = closestEnemy.position - playerGun.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                playerGun.rotation = Quaternion.Lerp(playerGun.rotation, targetRotation, Time.deltaTime * 10f);

                // Check if enough time has passed since the last fire

                if (Time.time >= nextFireTime)
                {
                    // Fire a bullet
                    FireBullet();
                    // Set the time for the next allowed fire
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (joystickMovement.joystickVector.y != 0)
        {
            rb.velocity = new Vector2(joystickMovement.joystickVector.x * playerSpeed, joystickMovement.joystickVector.y * playerSpeed);
            animator.SetBool("canRun", true);
        }

        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("canRun", false);
        }
    }
}
