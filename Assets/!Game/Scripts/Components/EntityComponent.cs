using UnityEngine;

namespace Components
{
    /// <summary>
    /// Component that holds the entity data.
    /// </summary>
    public class EntityComponent : MonoBehaviour
    {
        [SerializeField] private EntityData entityData;
        public EntityData EntityData => entityData;
    }
}
