using UnityEngine;

public class ButtonActivator : MonoBehaviour
{
    public GameObject SecondButton;
    public GameObject FirstButton;
    public GameObject player;
    public GameObject box;
    public GameObject ActivateBlock;
    public GameObject block;
    public GameObject ActivateBlock2;
    public GameObject block2;
    public bool isActivated = false;
    [SerializeField] private AudioClip ButtonSound;
    private AudioSource audioSource;
    private VibrateController vibrateController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player || collision.gameObject == box)
        {
            PlayButtonSound();
            HandleVibration();
            ToggleElements(true);
        }
    }

    private void PlayButtonSound()
    {
        if(audioSource != null)
        {
            audioSource.clip = ButtonSound;
            audioSource.Play();
            audioSource = null;
        }
        
    }

    private void HandleVibration()
    {
        if (vibrateController.isBoolTrue)
        {
            Taptic.Light();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
    }

    private void ToggleElements(bool activate)
    {
        FirstButton.SetActive(!activate);
        SecondButton.SetActive(activate);
        block.SetActive(!activate);
        ActivateBlock.SetActive(activate);
        if (block2 != null)
        {
            block2.SetActive(!activate);
            ActivateBlock2.SetActive(activate);
        }
    }
}