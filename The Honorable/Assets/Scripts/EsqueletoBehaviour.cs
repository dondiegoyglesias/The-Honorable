using System.Collections;
using UnityEngine;

public class EsqueletoBehaviour : MonoBehaviour
{

    [Header("Move Settings")]
    [SerializeField] float speed;

    [SerializeField] private GameObject player;
    [SerializeField] private float distanceWakeUp;
    [SerializeField] private float distanceAttack;

    [SerializeField] Vector3 posicaoInicial;

    private int esqueletoLife = 30;

    private bool attacking;
    private bool wakeUp;
    private bool AnimationIsDead;



    Collider2D collision;
    Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        WakeUp();
        Look();
        AnimatorManager();
    }

    void WakeUp()
    {
        if (wakeUp == false)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceWakeUp)
            {
                wakeUp = true;
            }

        }

        
        if (Vector2.Distance(transform.position, player.transform.position) < distanceAttack)
        {
             Attack();
        }

        else
        {
             Move();
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, speed * Time.deltaTime);
    }

    void Look()
    {
        if (player.gameObject.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (player.gameObject.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    void Attack()
    {
        StartCoroutine(Attacking());

    }

    IEnumerator Attacking()
    {
        attacking = true;
        yield return new WaitForSeconds(1.00f);
        attacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControlls>().TakeDamage(10);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
        {


        if (other.tag == "Sword")
        {
            esqueletoLife -= 10;
            print("Dano recebido");


            if (esqueletoLife <= 0)
            {
                AnimationIsDead = true;
                Destroy(gameObject, 0.5f);
            }
        }
    }


    void AnimatorManager()
    {
        animator.SetBool("pWakeUp", wakeUp);
        animator.SetBool("pDeath", AnimationIsDead);
    }
}
