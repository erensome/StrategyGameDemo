using BuildingSystem;
using EventBus;
using Factory;
using UI;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    [SerializeField] private Blueprint blueprint;
    private IBuildable currentBuildable;
    private bool isBlueprintActive;
    private float cellSize;
    
    private void Awake()
    {
        cellSize = GroundManager.Instance.CellSize;
        UIEventBus.OnProductionMenuItemSelected += OnProductionMenuItemSelected;
    }
    
    protected override void OnDestroy()
    {
        UIEventBus.OnProductionMenuItemSelected -= OnProductionMenuItemSelected;
        base.OnDestroy();
    }

    private void Update()
    {
        if (isBlueprintActive)
        {
            blueprint.Move();
            blueprint.CheckGround();
        }
    }

    public void HandleBuild()
    {
        if (currentBuildable == null) return;
        if (blueprint.IsAreaAvailable != true) return;
        
        blueprint.Mark(currentBuildable, false);
        currentBuildable.Build();
        isBlueprintActive = false;
        blueprint.DisposeBlueprint();
        
        GameEventBus.TriggerBuildingPlaced(currentBuildable);
        currentBuildable = null;
    }
    
    private void OnProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
    {
        if (currentBuildable != null) return; // Already building
        
        BuildingData buildingData = productionMenuItem.BuildingData;
        // Handle the building selection logic here
        Debug.Log($"Building selected: {buildingData.Name}");
        BuildingProduct building = BuildingFactory.Instance.Produce(buildingData.BuildingType, Vector3.zero);
        
        if (building != null)
        {
            currentBuildable = building.GetComponent<IBuildable>();
            isBlueprintActive = true;
            blueprint.Mark(currentBuildable, true);
            blueprint.CreateBlueprint(buildingData.Size, cellSize);
            building.transform.SetParent(blueprint.transform);
            building.transform.localPosition = Vector3.zero;
        }
    }
}
