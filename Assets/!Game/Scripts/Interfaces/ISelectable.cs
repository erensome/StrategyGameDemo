using UnityEngine;

public interface ISelectable
{
    GameObject SelectableObject { get; }
    
    /// <summary>
    /// Select the object
    /// </summary>
    void Select();

    /// <summary>
    /// Deselect the object
    /// </summary>
    void Deselect();
}