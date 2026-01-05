using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 currentVelocity;
    private Rigidbody2D slimeRigidbody;
    private LayerMask groundLayer;
    private int hazardLayer;
    private void Start()
    {
        groundLayer = LayerMask.NameToLayer("Ground");
        hazardLayer = LayerMask.NameToLayer("Hazard");
        slimeRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        currentVelocity = slimeRigidbody.linearVelocity;
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        currentVelocity.x = moveSpeed;
        slimeRigidbody.linearVelocity = currentVelocity;
    }

    /* private void OnTriggerExit2D(Collider2D collision) // its possible to use raycasts to check this too, probably its more expensive but i think this is more precise
    {
        if (collision.gameObject.layer == groundLayer)
        {
            moveSpeed = -moveSpeed;
            FlipSprite();
        }
    } */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == hazardLayer || collision.gameObject.layer == groundLayer)
        {
            moveSpeed = -moveSpeed;
            FlipSprite();
        }
    }


    private void FlipSprite()
    {
        Vector3 slimeScale = transform.localScale;
        slimeScale.x *= -1;
        transform.localScale = slimeScale;
    }

}
