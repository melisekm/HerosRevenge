using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");
    public Animator animator;
    public string gameOverSceneName = "Menu_ArenaSelection";
    public string scoreSceneName = "Menu_ArenaSelection";
    private PlayerContainer playerContainer;
    private string sceneToLoad;

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

    private void OnEnable()
    {
        PlayerUnit.OnPlayerDied += LoadGameOverScene;
        WinConditionChecker.OnWinConditionMet += LoadScoreScene;
    }

    private void OnDisable()
    {
        PlayerUnit.OnPlayerDied -= LoadGameOverScene;
        WinConditionChecker.OnWinConditionMet -= LoadScoreScene;
    }

    private void LoadScoreScene()
    {
        // TODO: Load score scene
        // wait for 2 seconds and then load the score scene
        StartCoroutine(FadeAfterDelay(1f, scoreSceneName));
    }


    private void LoadGameOverScene()
    {
        StartCoroutine(FadeAfterDelay(1f, gameOverSceneName));
    }

    private IEnumerator FadeAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        FadeToScene(sceneName);
    }

    public void FadeToScene(string sceneName)
    {
        sceneToLoad = sceneName;
        playerContainer.currentArena = playerContainer.GetArenaByName(sceneName);
        animator.SetTrigger(FadeOut);
    }

    public void ResetGame()
    {
        playerContainer.ResetGame();
        FadeToScene("Menu_ArenaSelection");
    }

    // In the animation event
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
        animator.SetTrigger(FadeIn);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}