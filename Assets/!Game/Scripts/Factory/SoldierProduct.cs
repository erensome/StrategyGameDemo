using Components;

public class SoldierProduct : ProductComponent
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
