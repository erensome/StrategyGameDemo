using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] private CameraController cameraController;
    private Camera mainCamera;
    private Vector2 moveInput;
    
    public bool IsMouseOverUI => EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

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
        HandleMouseInput();
        HandleKeyboardInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            GameStateManager.Instance.CurrentInputStateHandler.HandleLeftClick(mousePosition);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            GameStateManager.Instance.CurrentInputStateHandler.HandleRightClick(mousePosition);
        }
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            cameraController.ZoomIn();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            cameraController.ZoomOut();
        }
        
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveInput != Vector2.zero)
        {
            cameraController.Move(moveInput.x, moveInput.y);
        }
    }
    
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
}
