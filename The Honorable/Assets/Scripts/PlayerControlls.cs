using System.Collections;
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

    [Header("Attack Settings")]
    [SerializeField] private GameObject attackObject;
    [SerializeField] private Collider2D attackCollider;
    private bool attack;

    [Header("Life Settings")]
    [SerializeField] int life;
    [SerializeField] int maxLife = 100;
    private bool isHitting;
    private bool isDead;

    [Header("Knockback Settings")]
    public float knockbackForce;
    public float knockbackCount;
    public float knockbackTime;
    public bool isKnockbackRigth;


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
        life = maxLife;
    }

    
    void Update()
    {
        Move();
        Jump();
        Attack();
        AnimatorManager();

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
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnMove()
    {
        /* if (knockbackCount <= 0)
         {
             rb.linearVelocity = new Vector2(direction.x, rb.linearVelocityY);
         }
         else
         {
             if (isKnockbackRigth)
             {
                 rb.linearVelocity = new Vector2(-knockbackForce, knockbackForce);
             }
             if (!isKnockbackRigth)
             {
                 rb.linearVelocity = new Vector2(knockbackForce, knockbackForce);
             }
         }
        */
        if (knockbackCount <= 0)
        {
            rb.linearVelocity = new Vector2(direction.x, rb.linearVelocity.y);
        }
        else
        {
            if (isKnockbackRigth)
            {
                rb.linearVelocity = new Vector2(-knockbackForce, knockbackForce);
            }
            if (!isKnockbackRigth)
            {
                rb.linearVelocity = new Vector2(knockbackForce, knockbackForce);
            }
            knockbackCount -= Time.deltaTime;
        }
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

    void Attack()
    {
        attack = Input.GetButtonDown("Fire1");

        if (attack)
        {
            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        attackCollider.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life > 0)
        {
            isHitting = true;
            StartCoroutine(HitAnimation());
        }

        else if (life <= 0)
        {
            isDead = true;
            Destroy(gameObject, 1f);
        }
    }

    IEnumerator HitAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        isHitting = false;
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

    public int MovementRun()
    {
        return (int)direction.x;
    }

    public int MovementJump()
    {
        return (int)Vector2.up.y;
    }

    void AnimatorManager()
    {
        anim.SetInteger("pMove", MovementRun());
        anim.SetInteger("pJump", MovementJump());
        anim.SetBool("pGround", Grounded());
        anim.SetBool("pAttack", attack);
        anim.SetBool("pHitting", isHitting);
        anim.SetBool("pDeath", isDead);
    }
}
