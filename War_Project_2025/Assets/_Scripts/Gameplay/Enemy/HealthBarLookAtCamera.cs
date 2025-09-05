using UnityEngine;

public class HealthBarLookAtCamera : MonoBehaviour
{
    private Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;

       
        Vector3 direction = mainCamera.position - transform.position;

       
        direction.y = 0;

        
        if (direction != Vector3.zero)
            transform.forward = direction.normalized;
    }
}
