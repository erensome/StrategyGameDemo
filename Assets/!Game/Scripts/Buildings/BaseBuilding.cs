using Components;
using DG.Tweening;
using UnityEngine;

namespace Buildings
{
    public abstract class BaseBuilding : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] protected EntityComponent entityComponent;
        [SerializeField] protected DamageableComponent damageableComponent;
        [SerializeField] protected SelectableComponent selectableComponent;
        [SerializeField] protected BuildingProduct buildingProduct;
        [SerializeField] protected BuildableComponent buildableComponent;

        protected BuildingData buildingData;
        protected SpriteRenderer spriteRenderer;

        protected virtual void Awake()
        {
            buildingData = (BuildingData)entityComponent.EntityData;
            damageableComponent.MaxHealth = buildingData.Health;
            spriteRenderer = GetComponent<SpriteRenderer>();

            buildingProduct.OnSpawned += OnSpawned;
            buildingProduct.OnReturnedToPool += OnReturnedToPool;
            buildableComponent.OnBuild += OnBuilt;
            damageableComponent.OnDeath += OnDeath;
        }

        protected virtual void OnDestroy()
        {
            buildingProduct.OnSpawned -= OnSpawned;
            buildingProduct.OnReturnedToPool -= OnReturnedToPool;
            buildableComponent.OnBuild -= OnBuilt;
            damageableComponent.OnDeath -= OnDeath;
        }

        protected virtual void OnSpawned()
        {
            damageableComponent.ResetHealth();
            damageableComponent.SetActiveHealthBar(false);
        }

        protected virtual void OnBuilt()
        {
            damageableComponent.SetActiveHealthBar(true);
        }

        protected virtual void OnReturnedToPool() { }

        protected virtual void OnDeath()
        {
            spriteRenderer.DOFade(0, 0.6f).OnComplete(() =>
            {
                buildableComponent.Remove();
                ObjectPoolManager.Instance.ReturnObjectToPool(buildingData.Name, gameObject);
            });
        }
    }
}
