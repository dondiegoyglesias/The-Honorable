using UnityEngine;

public class OrbeMagoBehaviour : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;

        // Calcula direção só uma vez
        direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    // Caso a orbe use colisão normal
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControlls>().TakeDamage(10);
            Destroy(gameObject);
        }
    }

    // Caso a orbe use trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerControlls>().TakeDamage(10);
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
