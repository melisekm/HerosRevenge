using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : Singleton<SceneSystem>
{
    public PlayerContainer playerContainer;
    public Animator animator;
    public int levelToLoad;

    protected override void Awake()
    {
        if (Instance == null) {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
 
        } else if (Instance != this) {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            // copy data from old instance to new instance
            playerContainer = Instance.playerContainer;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        playerContainer ??= new PlayerContainer();
    }

    public void StartGame()
    {
        FadeToScene(1);
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
