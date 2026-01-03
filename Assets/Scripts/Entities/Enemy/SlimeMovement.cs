using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 currentVelocity;

    private BoxCollider2D flipCollider;
    private Rigidbody2D slimeRigidbody;
    private LayerMask groundLayer;


    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        slimeRigidbody = GetComponent<Rigidbody2D>();
        flipCollider = GetComponent<BoxCollider2D>();
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

    private void OnTriggerExit2D(Collider2D collision) // its possible to use raycasts to check this too, probably its more expensive but i think this is more precise
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
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
