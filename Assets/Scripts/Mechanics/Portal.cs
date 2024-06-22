using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform otherPortal; 
    public ParticleSystem otherPortalExplosion;
    private static HashSet<Collider2D> teleportedObjects = new HashSet<Collider2D>();
    public ParticleSystem explosionProtal;
    private GameObject trail;
    private AudioSource audioSource;
    [SerializeField] private AudioClip portalSound;

    void Start()
    {
        trail = GameObject.Find("trail");
        audioSource = GetComponent<AudioSource>();    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!teleportedObjects.Contains(other) && other.CompareTag("Player"))
        { 
            explosionProtal.Play();
            otherPortalExplosion.Play();
            StartCoroutine(Teleport(other));
        }

        if (!teleportedObjects.Contains(other) && other.CompareTag("box"))
        {
            explosionProtal.Play();
            otherPortalExplosion.Play();
            StartCoroutine(TeleportBox(other));
        }
    }

    private IEnumerator Teleport(Collider2D player)
    {
        
        SpriteRenderer spriteRenderer;
        spriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        VibrateController vibrateController;
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue == true)
        {
            Taptic.Light();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
        audioSource.clip = portalSound;
        audioSource.Play();

        teleportedObjects.Add(player);
        trail.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Invoke("StopTime", 2f);
        spriteRenderer.enabled = true;
        player.transform.position = otherPortal.position;
        yield return new WaitForSeconds(0.1f);
        trail.SetActive(true);
        teleportedObjects.Remove(player);
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue == true)
        {
            Taptic.Light();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
    }
    private IEnumerator TeleportBox(Collider2D player)
    {

        SpriteRenderer spriteRenderer;
        spriteRenderer = GameObject.FindGameObjectWithTag("box").GetComponent<SpriteRenderer>();
        VibrateController vibrateController;
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue == true)
        {
            Taptic.Light();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
        audioSource.clip = portalSound;
        audioSource.Play();

        teleportedObjects.Add(player);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Invoke("StopTime", 2f);
        spriteRenderer.enabled = true;
        player.transform.position = otherPortal.position;
        yield return new WaitForSeconds(0.1f);
        teleportedObjects.Remove(player);
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue == true)
        {
            Taptic.Light();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
    }

    private void StopTime(float time)
    {
        Time.timeScale = time;
    }
    private void ContinueTime(float time)
    {
        Time.timeScale = time;
    }
}
