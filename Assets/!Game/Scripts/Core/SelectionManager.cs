using Components;
using EventBus;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoSingleton<SelectionManager>
{
    public ISelectable CurrentSelectable => currentSelectable;
    private ISelectable currentSelectable;

    private const float SelectionScaleEffect = 1.2f;

    public void HandleSelection(Vector3 worldPosition)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // Ignore clicks on UI elements
            return;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider == null) // Clicked on empty space
        {
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