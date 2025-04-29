using BuildingSystem;
using EventBus;
using Factory;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    [SerializeField] private Blueprint blueprint;
    private IBuildable currentBuildable;
    private bool isBlueprintActive;
    
    private void Awake()
    {
        UIEventBus.OnBuildingSelected += OnBuildingSelected;
    }
    
    protected override void OnDestroy()
    {
        UIEventBus.OnBuildingSelected -= OnBuildingSelected;
        base.OnDestroy();
    }

    private void Update()
    {
        if (isBlueprintActive)
        {
            
        }
    }

    public void HandleBuild()
    {
        if (currentBuildable == null) return;
        
        currentBuildable.Build();
        isBlueprintActive = false;
        currentBuildable = null;
    }
    
    private void OnBuildingSelected(BuildingData buildingData)
    {
        // Handle the building selection logic here
        Debug.Log($"Building selected: {buildingData.Name}");
        
        BuildingProduct building = BuildingFactory.Instance.Produce(buildingData.BuildingType, Vector3.zero);
        
        if (building != null)
        {
            currentBuildable = building.GetComponent<IBuildable>();
            isBlueprintActive = true;
        }
    }
}
