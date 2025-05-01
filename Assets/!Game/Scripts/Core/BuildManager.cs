using BuildingSystem;
using EventBus;
using Factory;
using UI;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    [SerializeField] private Blueprint blueprint;
    private float cellSize;
    
    // Currently selected buildable object
    private IBuildable currentBuildable;
    private BuildingData currentBuildingData;
    
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
        blueprint.Move();
        blueprint.CheckGround();
    }

    public void HandleBuild()
    {
        if (currentBuildable == null) return;
        if (blueprint.IsAreaAvailable != true) return;
        
        blueprint.Mark(currentBuildable, false);
        currentBuildable.Build();
        blueprint.DisposeBlueprint();
        
        GameEventBus.TriggerBuildingPlaced(currentBuildable);
        currentBuildable = null;
    }
    
    private void OnProductionMenuItemSelected(ProductionMenuItem productionMenuItem)
    {
        if (currentBuildable != null)
        {
            DeselectOld();
        }

        if (productionMenuItem != null)
        {
            SelectNew(productionMenuItem.BuildingData);
        }
    }

    private void SelectNew(BuildingData buildingData)
    {
        currentBuildingData = buildingData;
        
        // Handle the building selection logic here
        BuildingProduct building = BuildingFactory.Instance.Produce(currentBuildingData.BuildingType, Vector3.zero);
        
        if (building != null)
        {
            currentBuildable = building.GetComponent<IBuildable>();
            blueprint.Mark(currentBuildable, true);
            blueprint.CreateBlueprint(currentBuildingData.Size, cellSize);
            building.transform.SetParent(blueprint.transform);
            building.transform.localPosition = new Vector3(0f, 0f, 1f); // Set the Z position to 1
        }
    }

    private void DeselectOld()
    {
        ObjectPoolManager.Instance.ReturnObjectToPool(currentBuildingData.Name, currentBuildable.BuildableObject);
        blueprint.DisposeBlueprint();
        currentBuildingData = null;
        currentBuildable = null;
    }
}
