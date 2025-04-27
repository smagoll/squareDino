using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

public class PoolSystem : MonoBehaviour
{
    [SerializeField]
    private Transform _projectileTransform;
    
    public static PoolSystem Instance;

    private Dictionary<string, object> _pools = new Dictionary<string, object>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ObjectPool<T> GetOrCreatePool<T>(T prefab) where T : Component
    {
        string poolKey = typeof(T).Name + "_" + prefab.name;

        if (_pools.TryGetValue(poolKey, out object existingPool))
        {
            return (ObjectPool<T>)existingPool;
        }

        Transform container = new GameObject(poolKey + "_Container").transform;
        container.SetParent(_projectileTransform);

        ObjectPool<T> newPool = new ObjectPool<T>(
            createFunc: () =>
            {
                T instance = Instantiate(prefab, container);
                instance.gameObject.name = prefab.name + "_pooled";
                return instance;
            },
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject)
        );

        _pools.Add(poolKey, newPool);
        return newPool;
    }
}