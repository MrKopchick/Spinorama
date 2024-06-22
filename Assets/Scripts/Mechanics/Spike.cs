using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{

    public float frezzeDuration = 1.0f;
    public float initialTimeScale;
    // Start is called before the first frame update
    public GameObject player;
    private void Start()
    {

        initialTimeScale = Time.timeScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            VibrateController vibrateController;
            vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
            if (vibrateController.isBoolTrue == true)
            {
                Taptic.Medium();
            }
            else
            {
                Debug.Log("мінус вібрація");
            }
            StartCoroutine(OnStopTimeAnimationFinish());
        }
    }
  
    public IEnumerator OnStopTimeAnimationFinish()
    {

       yield return new WaitForSeconds(0.05f);
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   

    IEnumerator UnfreezeTime(float duration)
    {
        yield return new WaitForSeconds(duration); 
        Time.timeScale = initialTimeScale; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
