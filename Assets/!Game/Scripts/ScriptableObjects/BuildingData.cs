using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Barracks,
    PowerPlant,
}

/// <summary>
/// This class is used to store the data for the buildings.
/// </summary>
[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/GameData/BuildingData", order = 2)]
public class BuildingData : EntityData
{
    public int Health;
    public BuildingType BuildingType; 
    public List<EntityData> ProductionItems;
}
