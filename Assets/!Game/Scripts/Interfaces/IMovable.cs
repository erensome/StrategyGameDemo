using UnityEngine;

public interface IMovable
{
    public float Speed { get; }
    public void Move(Vector3 targetPosition);
}
