using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    public int gameOverSceneIndex = 1;

    private void OnEnable()
    {
        PlayerUnit.OnPlayerDied += LoadGameOverScene;
    }

    private void OnDisable()
    {
        PlayerUnit.OnPlayerDied -= LoadGameOverScene;
    }

    private void LoadGameOverScene()
    {
        IEnumerator FadeAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            FadeToScene(gameOverSceneIndex);
        }

        StartCoroutine(FadeAfterDelay());
    }

    public void FadeToScene(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    // In the animation event
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        animator.SetTrigger("FadeIn");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}