using UnityEngine;

public interface IBuildable
{
    GameObject BuildableObject { get; }
    void Build();
    void Remove();
}