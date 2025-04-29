using UnityEngine;

/// <summary>
/// This is base class for all entities that can be placed on the grid.
/// </summary>
public class EntityData : ScriptableObject
{
    public string Name;
    public Vector2Int Size;
    public Sprite Icon;
}
