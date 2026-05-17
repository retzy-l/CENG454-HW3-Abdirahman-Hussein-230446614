// ObjectPool.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            IPoolable poolable = obj.GetComponent<IPoolable>();
            if (poolable != null) poolable.OnSpawn();
            return obj;
        }
        // If pool is empty, create a new one
        GameObject newObj = Instantiate(prefab, transform);
        IPoolable p = newObj.GetComponent<IPoolable>();
        if (p != null) p.OnSpawn();
        return newObj;
    }

    public void Return(GameObject obj)
    {
        IPoolable poolable = obj.GetComponent<IPoolable>();
        if (poolable != null) poolable.OnReturnToPool();
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}