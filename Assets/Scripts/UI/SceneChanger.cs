using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    private string sceneToLoad;
    public string gameOverSceneName = "Menu_ArenaSelection";
    private PlayerContainer playerContainer;

    private void OnEnable()
    {
        PlayerUnit.OnPlayerDied += LoadGameOverScene;
    }

    private void OnDisable()
    {
        PlayerUnit.OnPlayerDied -= LoadGameOverScene;
    }

    private void Start()
    {
        var playerContainerGo = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerGo && playerContainerGo.TryGetComponent(out PlayerContainer playerContainer))
        {
            this.playerContainer = playerContainer;
        }
        else
        {
            Debug.LogError("PlayerContainer not found");
        }
    }


    private void LoadGameOverScene()
    {
        IEnumerator FadeAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            FadeToScene(gameOverSceneName);
        }

        StartCoroutine(FadeAfterDelay());
    }

    public void FadeToScene(string sceneName)
    {
        sceneToLoad = sceneName;
        playerContainer.currentArena = sceneToLoad;
        animator.SetTrigger("FadeOut");
    }

    // In the animation event
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
        animator.SetTrigger("FadeIn");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}