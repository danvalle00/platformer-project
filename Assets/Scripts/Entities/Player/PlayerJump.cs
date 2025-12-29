using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private InputAction jumpInput;
    private Rigidbody2D playerRigidbody;
    private LayerMask groundLayer;

    private bool desiredJump;
    private Vector2 currentVelocity;

    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float upwardGravityMultiplier = 1f;
    [SerializeField] private float downwardGravityMultiplier = 2f;
    [SerializeField] private float timeToJumpApex = 0.4f;
    private readonly float defaultGravityScale = 1f;
    private float gravMultiplier = 1f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Start()
    {
        jumpInput = InputSystem.actions.FindAction("Jump");
    }


    private void Update()
    {
        SetPhysics();
        desiredJump = jumpInput.IsPressed();
    }

    private void FixedUpdate()
    {
        currentVelocity = playerRigidbody.linearVelocity;
        if (desiredJump)
        {
            Jump();
            playerRigidbody.linearVelocity = currentVelocity;
            return;
        }
        calculateGravity();
    }

    private void Jump()
    {
        if (OnGround() == false) return;
        float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * playerRigidbody.gravityScale * jumpHeight);

        if (currentVelocity.y > 0f)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - currentVelocity.y, 0f);
        }
        else if (currentVelocity.y < 0f)
        {
            jumpSpeed += Mathf.Abs(playerRigidbody.linearVelocity.y);
        }

        currentVelocity.y += jumpSpeed;
    }
    private void calculateGravity()
    {
        if (currentVelocity.y > 0.01f) // subindo
        {
            if (OnGround())
            {
                gravMultiplier = defaultGravityScale;

            }
            else
            {
                gravMultiplier = upwardGravityMultiplier;
            }
        }
        else if (currentVelocity.y < -0.01f) // descendo
        {
            if (OnGround())
            {
                gravMultiplier = defaultGravityScale;

            }
            else
            {
                gravMultiplier = downwardGravityMultiplier;
            }
        }
        else // no chão 
        {
            gravMultiplier = defaultGravityScale;
        }
    }

    private void SetPhysics()
    {
        Vector2 newGravity = new(0, (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex));
        playerRigidbody.gravityScale = (newGravity.y / Physics2D.gravity.y) * gravMultiplier;
    }

    private bool OnGround()
    {
        return playerRigidbody.IsTouchingLayers(groundLayer);
    }

}