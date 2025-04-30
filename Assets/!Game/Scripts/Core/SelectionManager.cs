using Components;
using EventBus;
using UnityEngine;

public class SelectionManager : MonoSingleton<SelectionManager>
{
    [SerializeField] private LayerMask selectableLayerMask;
    private ISelectable currentSelectable;
    private const float SelectionScaleEffect = 1.2f;

    public ISelectable CurrentSelectable => currentSelectable;
    public LayerMask SelectableLayerMask => selectableLayerMask;
    
    public void HandleSelection(Vector3 worldPosition)
    {
        if (InputManager.Instance.IsMouseOverUI) return; // Ignore clicks on UI elements
        
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero,  Mathf.Infinity, selectableLayerMask);
        
        if (hit.collider == null) // Clicked on empty space
        {
            DeselectCurrent();
            return;
        }
        
        Debug.Log(hit.collider.name);

        ISelectable selectable = hit.collider.GetComponent<ISelectable>();
        if (selectable == currentSelectable) return; // Already selected

        if (selectable != null) // Clicked on a selectable object
        {
            ChangeSelection(selectable);
        }
        else // Clicked on non-selectable object
        {
            DeselectCurrent();
        }
        
        GameStateManager.Instance.SetState(GameStateManager.GameState.Selecting);
    }

    private void ChangeSelection(ISelectable newSelectable)
    {
        if (currentSelectable == newSelectable) return; // Already selected

        DeselectCurrent();
        currentSelectable = newSelectable;
        currentSelectable.Select();
        SelectEffect();
        
        if (currentSelectable is MonoBehaviour component)
        {
            EntityComponent entityComponent = component.GetComponent<EntityComponent>();
            GameEventBus.TriggerEntitySelected(entityComponent.EntityData);
        }
    }

    private void DeselectCurrent()
    {
        if (currentSelectable == null) return;

        currentSelectable?.Deselect();
        DeselectEffect();
        currentSelectable = null;
        GameEventBus.TriggerEntitySelected(null); // Trigger event with null to clear selection
    }

    private void SelectEffect()
    {
        if (currentSelectable is MonoBehaviour component)
        {
            component.transform.localScale *= SelectionScaleEffect;
        }
    }

    private void DeselectEffect()
    {
        if (currentSelectable is MonoBehaviour component)
        {
            component.transform.localScale /= SelectionScaleEffect;
        }
    }
}