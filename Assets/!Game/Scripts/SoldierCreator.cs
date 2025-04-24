using System.Collections.Generic;
using UnityEngine;

public class SoldierCreator : MonoBehaviour
{
    private Stack<GameObject> soldiers = new Stack<GameObject>();
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var soldier = ObjectPoolManager.Instance.GetObjectFromPool(PoolType.Soldier);
            if (soldier != null)
            {
                soldier.transform.position = Random.insideUnitSphere * 5f;
                soldier.transform.rotation = Quaternion.identity;
                soldiers.Push(soldier);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (soldiers.Count == 0) return;
            ObjectPoolManager.Instance.ReturnObjectToPool(PoolType.Soldier, soldiers.Pop());
        }
    }
}
