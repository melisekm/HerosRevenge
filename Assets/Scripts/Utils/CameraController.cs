using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private bool isZooming;
    private Camera mainCamera;
    public bool zoomOnWin = true;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var position = target.position;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
        if (isZooming)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 1, Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        WinConditionChecker.OnWinConditionMet += ZoomToPlayer;
    }

    private void OnDisable()
    {
        WinConditionChecker.OnWinConditionMet -= ZoomToPlayer;
    }

    private void ZoomToPlayer()
    {
        if (zoomOnWin)
        {
            isZooming = true;
        }
    }
}