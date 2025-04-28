using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/GameData/BuildingData", order = 2)]
public class BuildingData : EntityData
{
    public int Health;
    public Sprite Icon;
    public GameObject Prefab;
}
