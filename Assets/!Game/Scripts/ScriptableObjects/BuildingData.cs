using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/GameData/BuildingData", order = 2)]
public class BuildingData : EntityData
{
    public int Health;
    public Image Icon;
    public GameObject Prefab;
}
