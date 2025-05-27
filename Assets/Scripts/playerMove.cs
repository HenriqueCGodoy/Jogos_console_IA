using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private KeyCode mLeftKey;
    [SerializeField] private KeyCode mRightKey;
    [SerializeField] private KeyCode jumpKey;
    private bool isGrounded;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(Input.GetKey(mLeftKey))
        {
            rb.AddForce(Vector3.left * acceleration, ForceMode.Force);
        }
        else if(Input.GetKey(mRightKey))
        {
            rb.AddForce(Vector3.right * acceleration, ForceMode.Force);
        }
        

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce( rb.linearVelocity + (Vector3.up * jumpForce), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
