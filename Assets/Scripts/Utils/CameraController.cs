using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private bool zoomOnWin = true;
    private bool isZooming = false;
    private Camera mainCamera;

    private void OnEnable()
    {
        WinConditionChecker.OnWinConditionMet += ZoomToPlayer;
    }

    private void OnDisable()
    {
        WinConditionChecker.OnWinConditionMet -= ZoomToPlayer;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void ZoomToPlayer()
    {
        if (zoomOnWin)
        {
            isZooming = true;
        }
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
}