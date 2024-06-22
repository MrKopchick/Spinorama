using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFalling;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFalling = false;
    }

    void Update()
    {
        isFalling = rb.velocity.y < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling && IsGrounded())
        {
            OnLand();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnLand()
    {
        VibrateController vibrateController = GameObject.Find("vibrate manager").GetComponent<VibrateController>();
        if (vibrateController.isBoolTrue)
        {
            Taptic.Medium();
        }
        else
        {
            Debug.Log("мінус вібрація");
        }
        Debug.Log("відбулася вібрація під час падіння");
    }
}
