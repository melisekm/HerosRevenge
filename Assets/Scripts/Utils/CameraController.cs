using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        var position = target.position;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}