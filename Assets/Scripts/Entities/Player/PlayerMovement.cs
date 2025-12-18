using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    // movement variables
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 desiredVelocity;
    private Vector2 currentVelocity;



    private InputSystem_Actions playerInput;
    private Vector2 movementInput;
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerInput = new InputSystem_Actions();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void Update()
    {
        movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        desiredVelocity = new Vector2(movementInput.x, 0f) * moveSpeed;
        FlipSprite();
    }

    private void FixedUpdate()
    {
        currentVelocity = playerRigidbody.linearVelocity;
        CalculateHorizontalMovement();
    }


    private void CalculateHorizontalMovement()
    {
        currentVelocity.x = desiredVelocity.x;
        playerRigidbody.linearVelocity = currentVelocity;
    }

    private void FlipSprite()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x = Mathf.Sign(movementInput.x);
        transform.localScale = playerScale;
    }


}