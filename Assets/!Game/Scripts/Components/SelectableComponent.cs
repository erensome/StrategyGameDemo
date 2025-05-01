using UnityEngine;

namespace Components
{
    /// <summary>
    /// Component that represents a selectable object in the game.
    /// </summary>
    // Requires a BoxCollider2D component to detect clicks with ray casting
    [RequireComponent(typeof(BoxCollider2D))]
    public class SelectableComponent : MonoBehaviour, ISelectable
    {
        public GameObject SelectableObject => gameObject;
        
        private const float SelectionScaleEffect = 1.2f;
        
        public void Select()
        {
            transform.localScale *= SelectionScaleEffect;
        }

        public void Deselect()
        {
            transform.localScale /= SelectionScaleEffect;
        }
    }
}
