using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerHurt : MonoBehaviour
{
    private bool isAlive = true;
    [SerializeField] private InputActionAsset inputActions;
    // private InputSystem_Actions actions; 
    private CapsuleCollider2D playerCapsuleCollider;
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;
    private LayerMask enemyLayer;
    private LayerMask hazardLayer;
    private Vector2 deathKnockback = new(0f, 20f);

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }


    private void Start()
    {
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        enemyLayer = LayerMask.GetMask("Enemy");
        hazardLayer = LayerMask.GetMask("Hazard");

    }

    private void Update()
    {
        if (playerCapsuleCollider.IsTouchingLayers(enemyLayer | hazardLayer) && isAlive)
        {
            isAlive = false;
            Death();
        }
    }
    private void Death()
    {
        inputActions.FindActionMap("Player").Disable(); // this sounds a good method for disable things
        // actions.Player.Disable(); // idk why this didnt work before, it should work the same way as above and i didnt had to string rfercne the action map name
        playerAnimator.SetTrigger("Dying");
        playerRigidbody.AddForce(deathKnockback, ForceMode2D.Impulse); // impulse ou velocity eis a questao
        GameSession.Instance.HandlePlayerDeaths();
    }




}