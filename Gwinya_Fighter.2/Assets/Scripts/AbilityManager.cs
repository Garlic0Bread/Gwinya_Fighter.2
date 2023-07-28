using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip enemyDeath;
    [SerializeField] private AudioClip gwinyaCollect;
    [SerializeField] private AudioClip abilityAvail;
    [SerializeField] private AudioClip shieldActive;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void enemyDeathSound()
    {
        audioSource.clip = enemyDeath;
        audioSource.Play();
    }

    public void gwinyaCollectSound()
    {
        audioSource.clip = gwinyaCollect;
        audioSource.Play();
    }

    public void abilitySpawnSound()
    {
        audioSource.clip = abilityAvail;
        audioSource.Play();
    }

    public void shieldActivated()
    {
        audioSource.clip = shieldActive;
        audioSource.Play();
    }
}
