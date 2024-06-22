using UnityEngine;

public class DelayButton : MonoBehaviour
{
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject player;
    public GameObject box;
    public GameObject ActivateBlock;
    public GameObject block;
    public GameObject ActivateBlock2;
    public GameObject block2;
    public bool isActivated = false;

    private AudioSource audioSource;
    [SerializeField] private AudioClip ButtonSound;

    private VibrateController vibrateController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player || collision.gameObject == box)
        {
            if (vibrateController.isBoolTrue)
            {
                Taptic.Light();
            }
            else
            {
                Debug.Log("мінус вібрація");
            }
            isActivated = true;
            PlayButtonSound();
            UpdateButtonsAndBlocks();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player || collision.gameObject == box)
        {
            if (vibrateController.isBoolTrue)
            {
                Taptic.Light();
            }
            else
            {
                Debug.Log("мінус вібрація");
            }
            isActivated = false;
            UpdateButtonsAndBlocks();
        }
    }

    private void PlayButtonSound()
    {
        audioSource.clip = ButtonSound;
        audioSource.Play();
    }

    private void UpdateButtonsAndBlocks()
    {
        FirstButton.SetActive(!isActivated);
        SecondButton.SetActive(isActivated);
        block.SetActive(!isActivated);
        ActivateBlock.SetActive(isActivated);
        if (block2 != null)
        {
            block2.SetActive(!isActivated);
            ActivateBlock2.SetActive(isActivated);
        }
    }
}
