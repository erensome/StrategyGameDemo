using UnityEngine;

public interface IBuildable
{
    GameObject BuildableObject { get; }
    
    /// <summary>
    /// Method that will be called when the object is built
    /// </summary>
    void Build();
    
    /// <summary>
    /// Method that will be called when the object is removed
    /// </summary>
    void Remove();
}