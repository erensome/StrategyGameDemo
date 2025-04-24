using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPool
{
    public string Name;
    public GameObject Prefab;
    public readonly List<GameObject> AvailableObjects = new();
}

public enum PoolType
{
    None,
    Soldier,
    Barracks,
    PowerPlant
}

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    private readonly List<Transform> poolTypeParents = new();
    // private readonly Dictionary<string, ObjectPool> objectPools = new();
    public List<ObjectPool> objectPools = new();

    private void Awake()
    {
        SetupPooledObjectsParent();
    }
    
    private void SetupPooledObjectsParent()
    {
        if (poolTypeParents.Count == 0)
        {
            GameObject pooledObjectsParent = new GameObject("PooledObjects");
            foreach (PoolType poolType in Enum.GetValues(typeof(PoolType)))
            {
                if (poolType == PoolType.None) continue;
                
                GameObject poolTypeParent = new GameObject(poolType.ToString());
                poolTypeParent.transform.SetParent(pooledObjectsParent.transform);
                poolTypeParents.Add(poolTypeParent.transform);
            }
        }
    }

    public GameObject GetObjectFromPool(PoolType poolType, Transform parent = null)
    {
        string poolName = poolType.ToString();
        ObjectPool objectPool = objectPools.Find(x => x.Name == poolName);
        if (objectPool == null)
        {
            Debug.LogError($"Object pool for {poolName} not found.");
            return null;
        }

        GameObject obj;
        if (objectPool.AvailableObjects.Count > 0)
        {
            obj = objectPool.AvailableObjects[0];
            objectPool.AvailableObjects.RemoveAt(0);
            obj.transform.SetParent(parent);
        }
        else
        {
            obj = Instantiate(objectPool.Prefab, parent);
        }
        
        // obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        
        IPoolable poolable = obj.GetComponent<IPoolable>();
        if (poolable != null)
        {
            poolable.Spawn();
        }

        return obj;
    }
    
    public void ReturnObjectToPool(PoolType poolType, GameObject obj)
    {
        string poolName = poolType.ToString();
        ObjectPool objectPool = objectPools.Find(x => x.Name == poolName);
        if (objectPool == null)
        {
            Debug.LogError($"Object pool for {poolName} not found.");
            return;
        }

        obj.SetActive(false);
        Transform parent = GetPoolTypeParent(poolType);
        obj.transform.SetParent(parent);
        objectPool.AvailableObjects.Add(obj);
        
        IPoolable poolable = obj.GetComponent<IPoolable>();
        if (poolable != null)
        {
            poolable.ReturnToPool();
        }
    }
    
    private Transform GetPoolTypeParent(PoolType poolType)
    {
        return poolTypeParents.Find(x => x.name == poolType.ToString());
    }
}
