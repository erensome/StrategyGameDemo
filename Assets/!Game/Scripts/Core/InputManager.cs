using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private AttackManager attackManager;
    
    private ISelectable currentSelectable;
    private IAttacker currentAttacker;
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
            selectionManager.HandleSelection(worldPosition);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPosition = GetWorldPosition();
            attackManager.HandleAttack(worldPosition);
        }
    }

    private Vector3 GetWorldPosition()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
    
    private void SelectObject()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            ISelectable selectable = hit.collider.GetComponent<ISelectable>();
            if (selectable != null)
            {
                currentSelectable?.Deselect();
                currentSelectable = selectable;
                currentSelectable.Select();
            }
            else
            {
                currentSelectable?.Deselect();
                currentSelectable = null;
            }
        }
    }

    private void Attack()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            var attacker = currentSelectable as IAttacker;
            if (damageable != null && attacker != null)
            {
                attacker.Attack(damageable);
            }
        }
    }
}
