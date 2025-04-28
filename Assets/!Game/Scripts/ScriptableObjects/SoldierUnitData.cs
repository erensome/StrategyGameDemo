using UnityEngine;

[CreateAssetMenu(fileName = "SoldierUnitData", menuName = "ScriptableObjects/GameData/SoldierUnitData", order = 1)]
public class SoldierUnitData : EntityData
{
    public int Health;
    public int Damage;
    public GameObject Prefab;
}
