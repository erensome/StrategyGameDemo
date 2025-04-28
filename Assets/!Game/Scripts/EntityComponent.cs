using UnityEngine;

public class EntityComponent : MonoBehaviour
{
    [SerializeField] private EntityData entityData;
    public EntityData EntityData => entityData;
}
