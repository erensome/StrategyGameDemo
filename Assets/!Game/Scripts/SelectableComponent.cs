using UnityEngine;

// Requires a BoxCollider2D component to detect clicks with ray casting
[RequireComponent(typeof(BoxCollider2D))]
public class SelectableComponent : MonoBehaviour, ISelectable
{
    public void Select()
    {
        // Implement selection logic here
        Debug.Log("Selected: " + gameObject.name);
    }

    public void Deselect()
    {
        // Implement deselection logic here
        Debug.Log("Deselected: " + gameObject.name);
    }
}
