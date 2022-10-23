using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private void Start()
    {
        
    }

    private void Update()
    {
        var position = target.position;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
