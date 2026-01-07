using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private static readonly WaitForSecondsRealtime _waitForSeconds1 = new(2f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }



    private IEnumerator LoadNextLevel()
    {
        yield return _waitForSeconds1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        ScenePersist.Instance.ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
