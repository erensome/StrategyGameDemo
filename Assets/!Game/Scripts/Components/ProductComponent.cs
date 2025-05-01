using System;
using UnityEngine;
using Factory;

namespace Components
{
    /// <summary>
    /// Component that represents a product (producible by a factory) in the game.
    /// </summary>
    public abstract class ProductComponent : MonoBehaviour, IProduct, IPoolable
    {
        public event Action OnProductInitialized;
        public event Action OnSpawned;
        public event Action OnReturnedToPool;
        
        public abstract void Initialize();
        public abstract void Spawn();
        public abstract void ReturnToPool();
        
        protected void TriggerOnProductInitialized()
        {
            OnProductInitialized?.Invoke();
        }
        
        protected void TriggerOnSpawned()
        {
            OnSpawned?.Invoke();
        }
        
        protected void TriggerOnReturnedToPool()
        {
            OnReturnedToPool?.Invoke();
        }
    }
}
