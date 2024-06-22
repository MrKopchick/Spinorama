using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springForce = 10f; // ���� �������
    public Vector2 springDirection = Vector2.up; // �������� ������� �� ������������� - �����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ��������, �� ����������� �������
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>(); // ��������� Rigidbody ������

            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(springDirection.normalized * springForce, ForceMode2D.Impulse); // ������������ ������ ��� ������������� ������ � ��������� ��������
            }
        }
    }
}
