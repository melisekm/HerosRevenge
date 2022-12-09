using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematics : MonoBehaviour
{
    private Transform playerSpawn;
    private Vector3 startPosition;
    private float elapsedTime;
    public float desiredDuration = 2f;
    public AnimationCurve curve;
    private void Start()
    {
        // find obejct with tag PlayerSpawn
        var playerSpawnGo = GameObject.FindWithTag("PlayerSpawn");
        if (playerSpawnGo)
        {
            playerSpawn = playerSpawnGo.transform;
        }
        startPosition = transform.position; 

    }

    // Source: https://www.youtube.com/watch?v=MyVY-y_jK1I
    private void Update()
    {
        if (elapsedTime > desiredDuration)
        {
            Destroy(this);
        }
        elapsedTime += Time.deltaTime;
        if (!playerSpawn) return;
        float percentageComplete = elapsedTime / desiredDuration;
    
        transform.position = Vector3.Lerp(startPosition, playerSpawn.position, curve.Evaluate(percentageComplete));
        
    }
}
