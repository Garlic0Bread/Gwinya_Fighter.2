using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] ability_items;
    [SerializeField] private Camera cam;
    [SerializeField] private int offsetY;
    [SerializeField] private int offsetX;

    int randomY;
    int randomX;
    GameObject spawnedItem;

    public void Spawn()
    {
        int randomObjectId = Random.Range(0, ability_items.Length);
        Vector2 getPos = GetRandomCoOrdinates();
        spawnedItem = Instantiate(ability_items[randomObjectId], getPos, Quaternion.identity) as GameObject;
    }

    Vector2 GetRandomCoOrdinates()
    {
        randomX = Random.Range(0 + offsetX, Screen.width - offsetX);
        randomY = Random.Range(0 + offsetY, Screen.width - offsetY);

        Vector2 coordinates = new Vector2(randomX, randomY);
        Vector2 screenToWorldPos = cam.ScreenToWorldPoint(coordinates);

        return screenToWorldPos;
    }
}
