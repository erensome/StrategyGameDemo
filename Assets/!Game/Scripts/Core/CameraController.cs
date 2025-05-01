using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool startAtWorldCenter = true;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float zoomSpeed = 15f;
    [SerializeField, Tooltip("x value must be min and y value must be max")]
    private Vector2 orthographicSizeBounds = new Vector2(3f, 25f);
    
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }
    }

    private void Start()
    {
        if (startAtWorldCenter)
        {
            Vector3 worldCenter = GroundManager.Instance.WorldCenterPoint;
            worldCenter.z = transform.position.z; // Keep the original z position
            transform.position = worldCenter;
        }
    }

    public void Move(float horizontalInput, float verticalInput)
    {
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0).normalized;
        transform.position += direction * (moveSpeed * Time.deltaTime);
    }

    // To zoom in orthographic size will be decreased
    public void ZoomIn()
    {
        float newSize = mainCamera.orthographicSize;
        newSize -= zoomSpeed * Time.deltaTime;
        newSize = Mathf.Clamp(newSize, orthographicSizeBounds.x, orthographicSizeBounds.y);
        mainCamera.orthographicSize = newSize;
    }
    
    // To zoom in orthographic size will be decreased
    public void ZoomOut()
    {
        float newSize = mainCamera.orthographicSize;
        newSize += zoomSpeed * Time.deltaTime;
        newSize = Mathf.Clamp(newSize, orthographicSizeBounds.x, orthographicSizeBounds.y);
        mainCamera.orthographicSize = newSize;
    }
}
