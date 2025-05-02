using System;
using UnityEngine;

namespace Components
{
    public class ProducerComponent : MonoBehaviour, IProducer
    {
        public event Action<EntityData> OnProduced;
        
        public void Produce(EntityData entityData)
        {
            OnProduced?.Invoke(entityData);
        }
    }
}
