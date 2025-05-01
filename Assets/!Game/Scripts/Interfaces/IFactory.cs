using UnityEngine;

namespace Factory
{
    public interface IFactory<T1, T2> where T1 : IProduct
    {
        /// <summary>
        /// Method to produce a product
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        T1 Produce(T2 type, Vector2 position);
    }
}
