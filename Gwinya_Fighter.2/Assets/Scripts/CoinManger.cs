using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManger : MonoBehaviour
{
    public int PlayerGwinyas = 0; // The player's initial coin count
    [SerializeField] private Image expBar;
    [SerializeField] private int exp;

    [SerializeField] private GameObject enableStore;

    public TMP_Text numCoins;

    // Function to buy an item
    public void BuyItem()
    {
       // if (playerCoins >= gwinyaPrice)
       // {
            // Deduct the item price from the player's coin count
        //    playerCoins -= gwinyaPrice;
         //   gwinyaAmount = gwinyaAmount + 1;
            // Perform item purchase logic (e.g., unlock power-up, increase player health, etc.)
            // Add your own code here to implement the specific effects of buying an item

          //  Debug.Log("Item purchased!");
          //  Debug.Log(gwinyaAmount);
      //  }
       // else
     //   {
      //      Debug.Log("Not enough coins to buy the item.");
       // }
    }

    // Function to add gwinyas to the player's gwinya count
    public void AddGwinya(int coinsToAdd)
    {
        PlayerGwinyas += coinsToAdd;
        Debug.Log(PlayerGwinyas);
    }

    public void UpdateExp(int expToGive)
    {
        exp += expToGive;
        expBar.fillAmount = exp / 100f;
        Enemy increaseEnemySpeed = FindObjectOfType<Enemy>();
        increaseEnemySpeed.IncreaseSpeed(0.05f);
    }

    private void Update()
    {
        numCoins.text = PlayerGwinyas.ToString();
        if(exp == 100)
        {
            print("exp max reached");
            Ability_Spawner spawnItem = FindObjectOfType<Ability_Spawner>();
            spawnItem.Spawn();

            exp = 0;
        }
    }
}

