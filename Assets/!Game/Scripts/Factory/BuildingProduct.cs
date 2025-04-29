using Components;

public class BuildingProduct : ProductComponent
{
    public override void Initialize()
    {
        TriggerOnProductInitialized();
    }

    public override void Spawn()
    {
        TriggerOnSpawned();
    }

    public override void ReturnToPool()
    {
        TriggerOnReturnedToPool();
    }
}
