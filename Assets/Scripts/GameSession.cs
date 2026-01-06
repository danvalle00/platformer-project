using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    private readonly int firstLevel = 0;
    [SerializeField] private int playerLives = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void HandlePlayerDeaths()
    {
        if (playerLives > 1)
        {
            playerLives--;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(firstLevel);
        Destroy(gameObject);
    }



}

