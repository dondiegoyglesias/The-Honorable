using UnityEngine;
using System.Collections;

public class MagoBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private Transform[] orbSpawnPoints;
    [SerializeField] private Transform[] teleportPoints;

    public int magoLife = 150;
    private bool AnimationIsDead;

    Collider2D collision;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [Header("Configuração de Ataques")]
    public float orbCooldown = 2f;
    public float restTime = 5f;
    public float explosionChargeTime = 3f;


    public bool isExplosion;
    public bool isHurt;
    public bool isOrbing;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collision = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(AttackPattern());
    }

    void Update()
    {
        Look();
        AnimatorManager();
    }

    void Look()
    {
        if (player.gameObject.transform.position.x < transform.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    IEnumerator AttackPattern()
    {
        while (!AnimationIsDead)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnOrb();
                isOrbing = true;
                yield return new WaitForSeconds(orbCooldown);
                isOrbing = false; 
            }

            yield return new WaitForSeconds(restTime);

            isExplosion = true;
            yield return new WaitForSeconds(explosionChargeTime);
            isExplosion = false;

            Teleport();
        }
    }

    void SpawnOrb()
    {
        if (orbSpawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, orbSpawnPoints.Length);
        Instantiate(orbPrefab, orbSpawnPoints[randomIndex].position, Quaternion.identity);
    }

    void Teleport()
    {
        if (teleportPoints.Length == 0) return;

        int randomIndex = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[randomIndex].position;
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
            magoLife -= 10;
            isHurt = true;

            Invoke(nameof(ResetHurt), 0.2f);

            if (magoLife <= 0)
            {
                AnimationIsDead = true;
                Destroy(gameObject, 0.5f);
            }
        }
    }

    void ResetHurt()
    {
        isHurt = false;
    }

    void AnimatorManager()
    {
        animator.SetBool("pExplosion", isExplosion);
        animator.SetBool("pOrb", isOrbing);
        animator.SetBool("pDeath", AnimationIsDead);
        animator.SetBool("pHurt", isHurt);
    }
}
