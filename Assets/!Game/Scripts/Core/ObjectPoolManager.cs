using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPool
{
    public string Name;
    public GameObject Prefab;
    public int PreallocateCount;
    public readonly List<GameObject> AvailableObjects = new();
}

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    [SerializeField] private List<ObjectPool> objectPools = new();

    private readonly List<Transform> poolParents = new();
    private readonly Dictionary<string, ObjectPool> objectPoolDictionary = new();
    
    private void Awake()
    {
        InitPoolParents();
        InitObjectPoolDictionary();
        PreallocateObjects();
    }
    
    private void InitPoolParents()
    {
        if (poolParents.Count == 0)
        {
            GameObject pooledObjectsParent = new GameObject("PooledObjects");
            foreach (ObjectPool objectPool in objectPools)
            {
                GameObject poolTypeParent = new GameObject(objectPool.Name);
                poolTypeParent.transform.SetParent(pooledObjectsParent.transform);
                poolParents.Add(poolTypeParent.transform);
            }
        }
    }

    private void InitObjectPoolDictionary()
    {
        objectPoolDictionary.Clear();
        foreach (ObjectPool objectPool in objectPools)
        {
            if (!string.IsNullOrEmpty(objectPool.Name) && objectPool.Prefab != null)
            {
                objectPoolDictionary[objectPool.Name] = objectPool;
            }
            else
            {
                Debug.LogWarning($"Invalid pool definition. Name: {objectPool.Name}, Prefab: {objectPool.Prefab}");
            }
        }
    }
    
    private void PreallocateObjects()
    {
        foreach (var pool in objectPools)
        {
            if (pool.PreallocateCount > 0 && pool.Prefab != null)
            {
                for (int i = 0; i < pool.PreallocateCount; i++)
                {
                    GameObject obj = Instantiate(pool.Prefab);
                    obj.SetActive(false);
                    
                    Transform parent = GetPoolTypeParent(pool);
                    if (parent != null)
                    {
                        obj.transform.SetParent(parent);
                    }
                    
                    pool.AvailableObjects.Add(obj);
                }
            }
        }
    }
    
    public GameObject GetObjectFromPool(string poolName, Transform parent = null)
    {
        if (string.IsNullOrEmpty(poolName))
        {
            Debug.LogError("Pool name cannot be null or empty.");
            return null;
        }
        
        if (!objectPoolDictionary.TryGetValue(poolName, out ObjectPool objectPool))
        {
            Debug.LogError($"Object pool for '{poolName}' not found.");
            return null;
        }
        
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
        }
        else
        {
            obj = Instantiate(objectPool.Prefab);
        }
        
        if (parent != null) obj.transform.SetParent(parent);
        obj.SetActive(true);
        
        IPoolable poolable = obj.GetComponent<IPoolable>();
        poolable?.Spawn();

        return obj;
    }
    
    public GameObject GetObjectFromPool(string poolName, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject obj = GetObjectFromPool(poolName, parent);
        if (obj != null)
        {
            obj.transform.SetPositionAndRotation(position, rotation);
        }
        return obj;
    }
    
    public void ReturnObjectToPool(string poolName, GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogWarning("Cannot return null object to pool.");
            return;
        }
        
        if (string.IsNullOrEmpty(poolName))
        {
            Debug.LogError("Pool name cannot be null or empty.");
            return;
        }
        
        if (!objectPoolDictionary.TryGetValue(poolName, out ObjectPool objectPool))
        {
            Debug.LogError($"Object pool for '{poolName}' not found.");
            return;
        }
        
        if (objectPool == null)
        {
            Debug.LogError($"Object pool for {poolName} not found.");
            return;
        }
        
        IPoolable poolable = obj.GetComponent<IPoolable>();
        poolable?.ReturnToPool();

        obj.SetActive(false);
        Transform parent = GetPoolTypeParent(objectPool);
        obj.transform.SetParent(parent);
        objectPool.AvailableObjects.Add(obj);
    }
    
    private Transform GetPoolTypeParent(ObjectPool objectPool)
    {
        return poolParents.Find(x => x.name == objectPool.Name);
    }
}
