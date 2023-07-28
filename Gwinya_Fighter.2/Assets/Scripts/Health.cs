using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxhealth = 100;
    [SerializeField] private int tokolosh_Exp;
    [SerializeField] private int pinky_Exp;

    [SerializeField] private GameObject pinkyDeathVFX;
    [SerializeField] private GameObject Gwinya;
    private GameObject spriteRender;
    [SerializeField] private Image healthBar;


    private void Start()
    {
        spriteRender = GameObject.FindGameObjectWithTag("SpriteToRend");
    }
    private void Update()
    {
        if (this.CompareTag("Player"))
        {
            healthBar.fillAmount = health / 100f;
        }
    }
    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.health -= amount;
        StartCoroutine(visualIndicator(Color.red));

        if (health < 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");
        }

        bool wouldBeOverMaHealth = health + amount > maxhealth;
        StartCoroutine(visualIndicator(Color.green));

        if (wouldBeOverMaHealth)
        {
            this.health = maxhealth;
        }

        else
        {
            this.health += amount;
        }

    }
    private void Die()
    {
        Destroy(gameObject);
        if (this.CompareTag("Enemy"))
        {
            Instantiate(pinkyDeathVFX, transform.position, Quaternion.identity);
            Instantiate(Gwinya, transform.position, Quaternion.identity);
            AbilityManager deathSound = FindObjectOfType<AbilityManager>();
            deathSound.enemyDeathSound();

            if (this.gameObject.layer == 3)
            {
                CoinManger addExp = FindObjectOfType<CoinManger>();
                if (addExp != null)
                {
                    addExp.UpdateExp(tokolosh_Exp);
                }
            }
            else if (this.gameObject.layer == 6)
            {
                CoinManger addExp = FindObjectOfType<CoinManger>();
                if (addExp != null)
                {
                    addExp.UpdateExp(pinky_Exp);
                }
            }
        }

        else if (this.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RecoverHealth") && this.gameObject.CompareTag("Player"))
        {
            Heal(25);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("IncreaseSpeed") && this.gameObject.CompareTag("Player"))
        {
            Player addSpeed = FindObjectOfType<Player>();
            addSpeed.SpeedBoost();
            Destroy(collision.gameObject);
        }
    }
    private IEnumerator visualIndicator (Color color)
    {
        if (this.gameObject.CompareTag("Player"))
        {
            spriteRender.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.15f);
            spriteRender.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.15f);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
