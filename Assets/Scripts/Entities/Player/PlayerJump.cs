using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerJump : MonoBehaviour
{
    private InputAction jumpInput;
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D feetCollider;
    private LayerMask groundLayer;

    private bool desiredJump;
    private Vector2 currentVelocity;

    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float upwardGravityMultiplier = 1f;
    [SerializeField] private float downwardGravityMultiplier = 2f;
    [SerializeField] private float timeToJumpApex = 0.4f;
    [SerializeField] private float speedLimit = 20f;
    private readonly float defaultGravityScale = 1f;
    private float gravMultiplier = 1f;


    /*
        Esse estilo de pulo é o mais simples possível, não tem coyote time, não tem jump buffering, não tem pulo duplo, não tem jump height variável de acordo com o tempo que o botão é pressionado.
        Com certeza tem mto espaço aqui pra melhora pq tem mto código tosco
    
    */

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
        feetCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        jumpInput = InputSystem.actions.FindAction("Jump"); // aprender o outro workflow de input usando
        // callback context, parece ser mais completo e simples 
    }


    private void Update()
    {
        SetPhysics();
        desiredJump = jumpInput.IsPressed(); // ja q n entendi se esse metodo eh é preciso ou nao
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
        CalculateGravity();
    }

    private void Jump()
    {
        if (OnGround())
        {
            desiredJump = false;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * playerRigidbody.gravityScale * jumpHeight); // famosa formula de queda livre v² = 2gh, gravidade é negativa e tem q multiplicar pela escala que tan o player
            if (currentVelocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - currentVelocity.y, 0f);
            }
            else if (currentVelocity.y < 0f)
            {
                jumpSpeed += Mathf.Abs(currentVelocity.y);
            }

            currentVelocity.y += jumpSpeed;

            desiredJump = false;
        }

    }
    private void CalculateGravity()
    {
        bool isGrounded = OnGround();
        if (playerRigidbody.linearVelocityY > 0.01f) // subindo
        {
            if (isGrounded)
            {
                gravMultiplier = defaultGravityScale;

            }
            else
            {
                gravMultiplier = upwardGravityMultiplier;
            }
        }
        else if (playerRigidbody.linearVelocityY < -0.01f) // descendo
        {
            if (isGrounded)
            {
                gravMultiplier = defaultGravityScale;

            }
            else
            {
                gravMultiplier = downwardGravityMultiplier;
            }
        }
        else
        {
            gravMultiplier = defaultGravityScale;
        }

        playerRigidbody.linearVelocity = new(currentVelocity.x, Mathf.Clamp(currentVelocity.y, -speedLimit, 100f)); // limitar a velocidade de queda 

    }

    private void SetPhysics()
    {
        Vector2 newGravity = new(0, (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex)); // h = 1/2 g t²  => g = 2h/t²
        playerRigidbody.gravityScale = (newGravity.y / Physics2D.gravity.y) * gravMultiplier;
    }

    private bool OnGround()
    {
        return feetCollider.IsTouchingLayers(groundLayer); // probably its better to use raycasts for this 
    }

}