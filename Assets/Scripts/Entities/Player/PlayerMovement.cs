using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float climbSpeed = 3f;
    private Vector2 desiredClimbVelocity;
    private Vector2 desiredVelocity;
    private Vector2 currentVelocity;
    private InputAction moveInput;

    private Vector2 movementInput;
    private Rigidbody2D playerRigidbody;

    // animator
    private Animator playerAnimator;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        moveInput = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        movementInput = moveInput.ReadValue<Vector2>();
        desiredVelocity = new Vector2(movementInput.x, 0f) * moveSpeed;
        desiredClimbVelocity = new Vector2(0f, movementInput.y) * climbSpeed;
        FlipSprite();
        AnimationHandler();

    }

    private void FixedUpdate()
    {
        currentVelocity = playerRigidbody.linearVelocity;
        CalculateHorizontalMovement();
        ClimbLadder();
    }


    private void CalculateHorizontalMovement()
    {
        currentVelocity.x = desiredVelocity.x;
        playerRigidbody.linearVelocity = currentVelocity;
    }

    private void FlipSprite()
    {
        if (movementInput.x == 0) return;
        Vector3 playerScale = transform.localScale;
        playerScale.x = Mathf.Sign(movementInput.x);
        transform.localScale = playerScale;
    }

    private void AnimationHandler()
    {
        if (movementInput.x != 0)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }
    private void ClimbLadder()
    {
        if (!playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            return;
        }
        playerRigidbody.gravityScale = 0f;
        currentVelocity.y = desiredClimbVelocity.y;
        playerRigidbody.linearVelocity = currentVelocity;

    }

}