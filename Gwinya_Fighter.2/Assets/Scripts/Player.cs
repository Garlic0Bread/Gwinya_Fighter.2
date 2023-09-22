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
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;

    [SerializeField] private Joystick_Movement joystickMovement;
    [SerializeField] private Transform playerGun; // Reference to the player's gun transform

    [SerializeField] private GameObject enableShield;
    [SerializeField] private GameObject gamePlayCanvas;
    [SerializeField] private GameObject gameShopCanvas;
    [SerializeField] private GameObject bulletPrefab; // Prefab of the bullet to be spawned
    [SerializeField] private GameObject phara1;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    public bool pharas_Active;
    public bool shieldOn;
    public Collider2D playerCollider;
    public Collider2D shieldCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        playerSpeed = playerOriginalSpeed;
        enableShield.SetActive(false);
        phara1.SetActive(false);
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

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
        // Mobile touch screen controls
        if (joystickMovement.joystickVector.y != 0)
        {
            rb.velocity = new Vector2(joystickMovement.joystickVector.x * playerSpeed, joystickMovement.joystickVector.y * playerSpeed);
            animator.SetBool("canRun", true);
        }else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("canRun", false);
        }


        if (joystickMovement.joystickVector.x > 0.35f) //run to the right
        {
            rb.velocity = new Vector2(joystickMovement.joystickVector.x * playerSpeed, joystickMovement.joystickVector.y * playerSpeed);

            animator.SetBool("rightRun", true);
        }
        else if (joystickMovement.joystickVector.x <= 0.35f)
        {
            animator.SetBool("rightRun", false);
        }


        if (joystickMovement.joystickVector.x < -0.35f) //run to the left
        {
            rb.velocity = new Vector2(joystickMovement.joystickVector.x * playerSpeed, joystickMovement.joystickVector.y * playerSpeed);
            animator.SetBool("leftRun", true);
        }
        else if (joystickMovement.joystickVector.x >= -0.35f)
        {
            animator.SetBool("leftRun", false);
        }

        //PC Controls
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("leftRun", true);
            rb.velocity = Vector2.left * playerSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("rightRun", true);
            rb.velocity = Vector2.right * playerSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("canRun", true);
            rb.velocity = Vector2.up * playerSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("canRun", true);
            rb.velocity = Vector2.down * playerSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinManger coin = FindObjectOfType<CoinManger>();
        if (collision.gameObject.CompareTag("Pharas") && coin.PlayerGwinyas > 4)
        {
            StartCoroutine(ActivatePharaGroup());

            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }
        if (collision.gameObject.CompareTag("Shield"))
        {
            StartCoroutine(EnableShield(collision));
            Destroy(collision.gameObject);
            shieldOn = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && shieldOn == false)
        {
            Health health = GetComponent<Health>();
            health.Damage(5);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy") && shieldOn == true)
        {
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("BossEnemy"))
        {
            Health health = GetComponent<Health>();
            health.Damage(8);
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

    IEnumerator EnableShield(Collider2D collision)
    {
        enableShield.SetActive(true);
        shieldCollider.enabled = true;
        playerCollider.enabled = false;
        yield return new WaitForSeconds(5);
        shieldOn = false;
        enableShield.SetActive(false);
        shieldCollider.enabled = false;
        playerCollider.enabled = true;
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


}
