using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;
    private PlayerMovement player;
    [SerializeField] private float bulletSpeed = 10f;
    private float xSpeed;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        bulletRigidbody = GetComponent<Rigidbody2D>();
        xSpeed = bulletSpeed * Mathf.Sign(player.transform.localScale.x);
    }

    private void FixedUpdate()
    {
        bulletRigidbody.linearVelocity = new(xSpeed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // destroi o inimigo
            Destroy(gameObject);
        }
        Destroy(gameObject, 1f);
    }


}

