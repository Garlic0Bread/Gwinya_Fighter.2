using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private void Update()
    {
        Player player = FindObjectOfType<Player>();
        GameObject[] pharas = GameObject.FindGameObjectsWithTag("Pharas");

        if (player.pharas_Active)
        {
            foreach(GameObject phara in pharas)
            {
                phara.SetActive(false);
            }
            player.pharas_Active = false;
        }
        if(player == null)
        {
            print("nada");
        }
    }
}
