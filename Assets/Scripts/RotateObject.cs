using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private AudioClip swipeSound;
    private AudioSource audioSource;

    public GameObject objectToRotate;
    public float rotationAngle = 90f;
    public float rotationDuration = 2f;
    public bool IsRotating = false;
    
    
    public GameObject objectToControl;
    public GameObject BoxToControl;
    public GameObject parentObject;
    public GameObject tapButtons; 

    private Rigidbody2D rb2d;
    private Rigidbody2D rbBox;
    public bool isToggle = false;
    public bool isStop = false;

    private Vector2 startTouchPositions;
    private Vector2 endTouchPositions;

    private Vector2 previousPosition;
    private Vector2 previousPositionBox;    

    public bool IsPause = false;
    private bool isMoving = false;

    [SerializeField] private float boxGravitation = 9f;

    private GameObject trail;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        trail = GameObject.Find("trail");
        if (objectToControl != null)
        {
            rb2d = objectToControl.GetComponent<Rigidbody2D>();
            previousPosition = rb2d.position;
        }

        if (BoxToControl != null)
        {
            rbBox = BoxToControl.GetComponent<Rigidbody2D>();
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public async void RotateObjectRight()
    {
        int tapsControl = PlayerPrefs.GetInt("TapsControl", 0);
        if (!isStop && !IsRotating)
        {
            if (objectToControl.GetComponent<Rigidbody2D>().velocity.magnitude == 0 )
            {
                VibrateController vibrateController;
                vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
                if (vibrateController.isBoolTrue == true)
                {
                    Taptic.Medium();
                }
                else
                {
                    Debug.Log("���� �������");
                }
                isToggle = true;
                IsRotating = true;
                Vector3 targetRotation = objectToRotate.transform.rotation.eulerAngles;
                targetRotation.z += rotationAngle;
                await RotateObjectSmooth(targetRotation);
            }
        }
        else if (isStop)
        {
            Debug.Log("������!");
        }
    }


    

    public async void RotateObjectLeft()
    {
        
        if (!isStop && !IsRotating)
        {
            if (objectToControl.GetComponent<Rigidbody2D>().velocity.magnitude == 0)
            {
                VibrateController vibrateController;
                vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
                if (vibrateController.isBoolTrue == true)
                {
                    Taptic.Medium();
                }
                else
                {
                    Debug.Log("���� �������");
                }
                isToggle = true;
                IsRotating = true;
                Vector3 targetRotation = objectToRotate.transform.rotation.eulerAngles;
                targetRotation.z -= rotationAngle;
                await RotateObjectSmooth(targetRotation);
            }
        }
        else if (isStop)
        {
            Debug.Log("������!");
        }
    }

    public void Left()
    {
        int tapsControl = PlayerPrefs.GetInt("TapsControl", 0);
        if (tapsControl == 1 && !isStop && !IsRotating)
        {
            RotateObjectLeft();
        }
    }

    public void Right()
    {
        int tapsControl = PlayerPrefs.GetInt("TapsControl", 0);
        if (tapsControl == 1 && !isStop && !IsRotating)
        {
            RotateObjectRight();
        }
    }



    private async Task RotateObjectSmooth(Vector3 targetRotation)
    {
        Vector3 initialRotation = objectToRotate.transform.rotation.eulerAngles;
        float remainingTime = rotationDuration;

        audioSource.clip = swipeSound;
        audioSource.Play();

        trail.SetActive(false); // Disable the trail at the beginning of the rotation

        while (remainingTime > 0)
        {
            float progress = Mathf.SmoothStep(0, 1, 1 - (remainingTime / rotationDuration)); // ������������ SmoothStep ��� ����� ������������
            Vector3 currentRotation = Vector3.Lerp(initialRotation, targetRotation, progress);
            if (objectToRotate != null)
            {
                objectToRotate.transform.rotation = Quaternion.Euler(currentRotation);
            }
            else
            {
                return;
            }

            rb2d.gravityScale = 0.0f;
            rbBox.gravityScale = 0.0f;
            objectToControl.transform.parent = parentObject.transform;
            BoxToControl.transform.parent = parentObject.transform;

            remainingTime -= Time.deltaTime;
            await Task.Yield();
        }

        if (objectToRotate != null)
        {
            objectToRotate.transform.rotation = Quaternion.Euler(targetRotation);
        }

        rb2d.gravityScale = 9f;
        rbBox.gravityScale = boxGravitation;
        objectToControl.transform.parent = null;
        BoxToControl.transform.parent = null;
        IsRotating = false;
        isToggle = false;

        trail.SetActive(true); // Re-enable the trail at the end of the rotation
    }

    private void Update()
    {
        int tapsControl = PlayerPrefs.GetInt("TapsControl", 0);
        GameObject tapButtons = GameObject.Find("TapsControl");
        if (tapButtons != null)
        {
            tapButtons.SetActive(tapsControl == 1);
        }

        if (objectToControl == null || BoxToControl == null)
        {
            return;
        }


        Vector2 currentPosition = objectToControl.transform.position;
        Vector2 currentPositionBox = BoxToControl.transform.position;

        if (currentPosition != previousPosition && currentPositionBox != previousPositionBox)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        previousPosition = currentPosition;

        Time.timeScale = 1f;

        int swipeControl = PlayerPrefs.GetInt("SwipeControl", 0);

        if (swipeControl == 1)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPositions = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPositions = Input.GetTouch(0).position;

                if (endTouchPositions.x < startTouchPositions.x)
                {
                    if (IsPause == false)
                    {
                        RotateObjectRight();
                    }
                }

                if (endTouchPositions.x > startTouchPositions.x)
                {
                    if (IsPause == false)
                    {
                        RotateObjectLeft();
                    }
                }
            }
        }
    }
}
