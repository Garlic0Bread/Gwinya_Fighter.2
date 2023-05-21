using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManger : MonoBehaviour
{
    public int playerCoins = 0; // The player's initial coin count
    public int gwinyaPrice = 10; // The price of each item in the store
    public int gwinyaAmount;

    public TMP_Text numCoins;
    public TMP_Text numGwinyas;

    // Function to buy an item
    public void BuyItem()
    {
        if (playerCoins >= gwinyaPrice)
        {
            // Deduct the item price from the player's coin count
            playerCoins -= gwinyaPrice;
            gwinyaAmount = gwinyaAmount + 1;
            // Perform item purchase logic (e.g., unlock power-up, increase player health, etc.)
            // Add your own code here to implement the specific effects of buying an item

            Debug.Log("Item purchased!");
            Debug.Log(gwinyaAmount);
        }
        else
        {
            Debug.Log("Not enough coins to buy the item.");
        }
    }

    // Function to add coins to the player's coin count
    public void AddCoins(int coinsToAdd)
    {
        playerCoins += coinsToAdd;
        Debug.Log(playerCoins);
        BuyItem();
    }

    private void Update()
    {
        numCoins.text = playerCoins.ToString();
        numGwinyas.text = gwinyaAmount.ToString();
        if (gwinyaAmount == 3)
        {
            SceneManager.LoadScene(1);
        }
        
    }
}

