public class Systems : Singleton<Systems>
{
    // persistent singleton
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}