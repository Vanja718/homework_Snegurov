using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ��������� �������� ��� �������������
    }

    void Update()
    {
        // �������� ���������� �� �����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // �������� ���� � ����������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ������������ ����������� ��������
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // ������
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // ��������� �������� � FixedUpdate ��� ������� ������
        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 moveVelocity = moveDirection * moveSpeed;
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }
    }

    // ������������ ���� ��� �������
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}