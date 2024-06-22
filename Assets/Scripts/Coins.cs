using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    private bank bank; // Посилання на скрипт банку
    [SerializeField] private AudioClip coinSound;
    private AudioSource audioSource;
    public int coinsCountForLevel;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bank = FindObjectOfType<bank>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin"))
        {
            coinsCountForLevel++;   
            audioSource.clip = coinSound;   
            audioSource.Play();
            Taptic.Light();
            bank.AddCoin(1);
            Destroy(other.gameObject);
        }
    }
}
