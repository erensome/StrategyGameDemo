using UnityEngine;

public enum SoldierType
{
    Alpha,
    Beta,
    Delta
}

/// <summary>
/// This class is used to store the data for the soldier unit.
/// </summary>
[CreateAssetMenu(fileName = "SoldierUnitData", menuName = "ScriptableObjects/GameData/SoldierUnitData", order = 1)]
public class SoldierUnitData : EntityData
{
    public int Health;
    public int Damage;
    public SoldierType SoldierType;
}
