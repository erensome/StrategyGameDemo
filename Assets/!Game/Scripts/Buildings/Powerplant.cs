using UnityEngine;
using Components;

namespace Buildings
{
    [RequireComponent(typeof(EntityComponent))]
    [RequireComponent(typeof(DamageableComponent))]
    [RequireComponent(typeof(SelectableComponent))]
    [RequireComponent(typeof(BuildingProduct))]
    [RequireComponent(typeof(BuildableComponent))]
    public class Powerplant : BaseBuilding
    {
        
    }
}
