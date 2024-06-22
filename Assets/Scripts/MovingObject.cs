using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private float speed;

    // ���������� �������� ���� ��'����
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Update()
    {
        // ������ ��'��� ������
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
