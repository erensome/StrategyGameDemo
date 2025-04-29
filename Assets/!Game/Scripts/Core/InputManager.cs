using UnityEngine;

public class InputManager : MonoBehaviour
{
    
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = GetWorldPosition();
            SelectionManager.Instance.HandleSelection(worldPosition);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPosition = GetWorldPosition();
            AttackManager.Instance.HandleAttack(worldPosition);
            // moveManager.HandleMove(worldPosition);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 worldPosition = GetWorldPosition();
            MoveManager.Instance.HandleMove(worldPosition);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            BuildManager.Instance.HandleBuild();
        }
    }

    private Vector3 GetWorldPosition()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
}
