using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] private AudioClip coinSoundFX;
    private bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinSoundFX, transform.position);
            Destroy(gameObject);
        }
    }
}
