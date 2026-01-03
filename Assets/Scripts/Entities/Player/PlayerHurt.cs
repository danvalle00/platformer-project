using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerHurt : MonoBehaviour
{
    private bool isAlive = true;
    [SerializeField] private InputActionAsset inputActions;
    // private InputSystem_Actions actions; 
    private Collider2D playerCollider;
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;
    private LayerMask enemyLayer;
    private Vector2 deathKnockback = new(-100f, 20f);

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        enemyLayer = LayerMask.GetMask("Enemy");

    }

    private void Update()
    {
        if (playerCollider.IsTouchingLayers(enemyLayer) && isAlive)
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
        playerCollider.enabled = false;

    }


}



