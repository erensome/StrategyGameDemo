public interface IPoolable
{
    /// <summary>
    /// Method that is called when the object is spawned from the pool
    /// </summary>
    void Spawn();
    
    /// <summary>
    /// Method that is called when the object is returned to the pool
    /// </summary>
    void ReturnToPool();
}
