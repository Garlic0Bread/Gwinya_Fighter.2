using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float deactivatePharaTime;

    private void Update()
    {
        if (this.gameObject.CompareTag("Phara"))
        {
            StartCoroutine(deactivatePhara());
        }
        else
        {
            Destroy(gameObject, waitTime);
        }
    }

    IEnumerator deactivatePhara()
    {
        yield return new WaitForSeconds(deactivatePharaTime);
        gameObject.SetActive(false);
    }
}
