using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/GameData/BuildingData", order = 2)]
public class BuildingData : ScriptableObject
{
    public string Name;
    public int Health;
    public Image Icon;
    public Vector2Int Size;
    public GameObject Prefab;
}
