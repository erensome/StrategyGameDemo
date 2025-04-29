using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Barracks,
    PowerPlant,
}

[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/GameData/BuildingData", order = 2)]
public class BuildingData : EntityData
{
    public int Health;
    public BuildingType BuildingType; 
    public List<EntityData> ProductionItems;
}
