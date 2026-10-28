using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlls : MonoBehaviour
{

    [Header("Move Settings")]
    [SerializeField] float moveSpeed;
    private Vector2 direction;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce;
    [SerializeField] float currentJumpTime;
    [SerializeField] float jumpTimeDuration;
    [SerializeField] private Transform sensorGround;
    [SerializeField] private Vector3 sensorSize;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float localGravity = 1.0f;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        rb.gravityScale = localGravity;
    }

    
    void Update()
    {
        Move();
        Jump();

    }

    void FixedUpdate()
    {
        OnMove();
        OnJump();
    }

    void Move()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnMove()
    {
        rb.linearVelocity = new Vector2(direction.x, rb.linearVelocityY);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded() == true)
        {
            currentJumpTime = jumpTimeDuration;
        }
        else if (Input.GetButton("Jump") && currentJumpTime > 0.0f)
        {
            currentJumpTime -= Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            currentJumpTime = 0.0f;
        }
    }

    void OnJump()
    {
        if (currentJumpTime > 0.0f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool Grounded()
    {
        return Physics2D.OverlapBox(sensorGround.position, sensorSize, 0, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(sensorGround.position, sensorSize);
    }

    void AnimatorManager()
    {

    }
}
