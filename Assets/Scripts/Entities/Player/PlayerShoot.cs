using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShoot : MonoBehaviour
{
    private InputAction shootInput;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private GameObject bulletPrefab;


    private void Start()
    {
        shootInput = InputSystem.actions.FindAction("Attack");
        gunTransform = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        if (shootInput.WasPressedThisFrame())
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
    }

}
