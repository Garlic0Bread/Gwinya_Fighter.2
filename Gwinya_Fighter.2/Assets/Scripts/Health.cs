using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxhealth = 100;
    [SerializeField] private GameObject pinkyDeathVFX;
    [SerializeField] private Image healthBar;
    public ScreenShake screenShake;

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.health -= amount;
        StartCoroutine(visualIndicator(Color.red));

        if (this.CompareTag("Player"))
        {
            healthBar.fillAmount = health / 100f;
            screenShake.Shake();
        }
            


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
        healthBar.fillAmount = health;

    }

    private void Die()
    {
        Destroy(gameObject);
        if (this.CompareTag("Enemy"))
        {
            CoinManger coinManager = FindObjectOfType<CoinManger>();
            if(coinManager !!= null)
            {
                coinManager.AddCoins(1);
            }
            Instantiate(pinkyDeathVFX, transform.position, Quaternion.identity);
        }

        else if (this.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
    }
    private IEnumerator visualIndicator (Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
