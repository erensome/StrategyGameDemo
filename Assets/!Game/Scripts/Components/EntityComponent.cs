using UnityEngine;

namespace Components
{
    public class EntityComponent : MonoBehaviour
    {
        [SerializeField] private EntityData entityData;
        public EntityData EntityData => entityData;
    }
}
