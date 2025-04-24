using UnityEngine;

public interface IPoolable
{
    void Spawn();
    void ReturnToPool();
}
