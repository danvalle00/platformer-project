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
    private LayerMask climbingLayer;
    private LayerMask groundLayer;


    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        climbingLayer = LayerMask.GetMask("Climbing");
        groundLayer = LayerMask.GetMask("Ground");
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
        ClimbLadder();
        MoveHorizontal();
    }

    private void MoveHorizontal()
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

    private void ClimbLadder()
    {
        if (!playerRigidbody.IsTouchingLayers(climbingLayer))
        {
            return;
        }
        playerRigidbody.gravityScale = 0f;
        currentVelocity.y = desiredClimbVelocity.y;
        playerRigidbody.linearVelocity = currentVelocity;
    }

    private void AnimationHandler() // checar state machine para melhorar esse codigo
    {
        bool isRunning = Mathf.Abs(playerRigidbody.linearVelocityX) > Mathf.Epsilon;
        bool isClimbing = playerRigidbody.IsTouchingLayers(climbingLayer);
        bool isGrounded = playerRigidbody.IsTouchingLayers(groundLayer);
        playerAnimator.SetBool("isRunning", isRunning && isGrounded);
        playerAnimator.SetBool("isClimbing", isClimbing && !isGrounded);
    }



}


