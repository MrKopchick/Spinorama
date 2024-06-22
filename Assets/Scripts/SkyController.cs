using System.Collections;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnInterval = 2f;
    public float speed = 5f;
    public float objectLifetime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            GameObject newObj = Instantiate(objectPrefab, transform.position, Quaternion.identity);
            MovingObject movingObject = newObj.GetComponent<MovingObject>();
            if (movingObject != null)
            {
                movingObject.SetSpeed(speed);
            }

            Destroy(newObj, objectLifetime);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
