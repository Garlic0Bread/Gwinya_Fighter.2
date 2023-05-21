using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxhealth = 100;
    [SerializeField] private GameObject pinkyDeathVFX;

    public void Damage(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.health -= amount;
        StartCoroutine(visualIndicator(Color.red));

        if(health < 0)
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
            CoinManger coinManager = FindObjectOfType<CoinManger>();
            if(coinManager !!= null)
            {
                coinManager.AddCoins(1);
            }
            Instantiate(pinkyDeathVFX, transform.position, Quaternion.identity);
        }
    }
 
    private IEnumerator visualIndicator (Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
