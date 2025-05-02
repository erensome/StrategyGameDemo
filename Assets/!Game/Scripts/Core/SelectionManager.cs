using Components;
using EventBus;
using UI;
using UnityEngine;

public class SelectionManager : MonoSingleton<SelectionManager>
{
    [SerializeField] private LayerMask selectableLayerMask;
    private ISelectable currentSelectable;

    public ISelectable CurrentSelectable => currentSelectable;
    public LayerMask SelectableLayerMask => selectableLayerMask;

    private void Awake()
    {
        UIEventBus.OnProductionMenuItemSelected += OnProductionMenuItemSelected;
    }

    protected override void OnDestroy()
    {
        UIEventBus.OnProductionMenuItemSelected -= OnProductionMenuItemSelected;
        base.OnDestroy();
    }

    /// <summary>
    /// If the user clicks on the UI object then deselect the current entity selection.
    /// </summary>
    /// <param name="productionMenuItem"></param>
    private void OnProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
    {
        if (productionMenuItem == null) return; // Double click on the same object
        DeselectCurrent();
    }

    public void HandleSelection(Vector3 worldPosition)
    {
        if (InputManager.Instance.IsMouseOverUI) return; // Ignore clicks on UI elements
        
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero,  Mathf.Infinity, selectableLayerMask);
        
        if (hit.collider == null) // Clicked on empty space
        {
            GameEventBus.TriggerEntitySelected(null); // Trigger event with null to clear selection
            DeselectCurrent();
            return;
        }
        
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
        currentSelectable?.Select();
        
        if (currentSelectable is MonoBehaviour component)
        {
            EntityComponent entityComponent = component.GetComponent<EntityComponent>();
            GameEventBus.TriggerEntitySelected(entityComponent);
        }
    }

    private void DeselectCurrent()
    {
        if (currentSelectable == null) return;

        currentSelectable?.Deselect();
        currentSelectable = null;
    }
}