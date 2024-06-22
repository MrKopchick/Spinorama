using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springForce = 10f; // Сила пружини
    public Vector2 springDirection = Vector2.up; // Напрямок пружини за замовчуванням - вгору

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Перевірка, чи доторкнувся гравець
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>(); // Отримання Rigidbody гравця

            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(springDirection.normalized * springForce, ForceMode2D.Impulse); // Використання фізики для відштовхування гравця у вказаному напрямку
            }
        }
    }
}
