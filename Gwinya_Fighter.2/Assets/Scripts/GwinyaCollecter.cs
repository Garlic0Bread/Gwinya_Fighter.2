using UnityEngine;

public class GwinyaCollecter : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinManger addGwinya = FindObjectOfType<CoinManger>();
            if (addGwinya! != null)
            {
                addGwinya.AddGwinya(1);
                Destroy(gameObject);
            }

            AbilityManager deathSound = FindObjectOfType<AbilityManager>();
            deathSound.gwinyaCollectSound();
        }
    }
}
