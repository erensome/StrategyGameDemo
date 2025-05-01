using UnityEngine;

public interface IMovable
{
    public float Speed { get; }
    
    /// <summary>
    /// Move the object to the target position
    /// </summary>
    /// <param name="targetPosition"></param>
    public void Move(Vector3 targetPosition);
}
