using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private Transform target;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        // FollowPlayer();
    }
    
    private void FollowPlayer()
    {
        // if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        // {
        //     transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        // }
    }
}
