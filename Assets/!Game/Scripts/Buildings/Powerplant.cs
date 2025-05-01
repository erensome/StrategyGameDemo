using UnityEngine;
using Components;
using DG.Tweening;

[RequireComponent(typeof(EntityComponent))]
[RequireComponent(typeof(DamageableComponent))]
[RequireComponent(typeof(SelectableComponent))]
[RequireComponent(typeof(BuildingProduct))]
[RequireComponent(typeof(BuildableComponent))]
public class Powerplant : MonoBehaviour
{
    private BuildingData buildingData;
    
    [Header("Components")]
    [SerializeField] private EntityComponent entityComponent;
    [SerializeField] private DamageableComponent damageableComponent;
    [SerializeField] private SelectableComponent selectableComponent;
    [SerializeField] private BuildingProduct buildingProduct;
    [SerializeField] private BuildableComponent buildableComponent;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        buildingData = (BuildingData)entityComponent.EntityData;
        damageableComponent.MaxHealth = buildingData.Health;
        buildableComponent.OnBuild += HandlePowerplantBuild;
        damageableComponent.OnDeath += HandlePowerplantDestroy;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnDestroy()
    {
        buildableComponent.OnBuild -= HandlePowerplantBuild;
        damageableComponent.OnDeath -= HandlePowerplantDestroy;
    }

    private void HandlePowerplantBuild()
    {
        damageableComponent.SetActiveHealthBar(true);
    }
    
    private void HandlePowerplantDestroy()
    {
        // common death animation
        spriteRenderer.DOFade(0, 0.6f).OnComplete(() =>
        {
            buildableComponent.Remove();
            ObjectPoolManager.Instance.ReturnObjectToPool(buildingData.Name, gameObject);
        });
    }
}
