using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private float speed;

    // Встановлює швидкість руху об'єкта
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Update()
    {
        // Рухаємо об'єкт вправо
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
