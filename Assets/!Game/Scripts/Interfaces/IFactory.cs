using UnityEngine;

namespace Factory
{
    public interface IFactory<T1, T2> where T1 : IProduct
    {
        T1 Produce(T2 type, Vector2 position);
    }
}
