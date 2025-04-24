using UnityEngine;

[CreateAssetMenu(fileName = "SoldierUnitData", menuName = "ScriptableObjects/GameData/SoldierUnitData", order = 1)]
public class SoldierUnitData : ScriptableObject
{
    public string Name;
    public int Health;
    public int Damage;
    public GameObject Prefab;
}
