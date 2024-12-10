using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<string, IObjectPool<IPoolable>> _pools;

    void Awake()
    {
        Instance = this;
        _pools = new Dictionary<string, IObjectPool<IPoolable>>();
    }

    public void CreatePool<T>(string key, T prefab, int poolSize) where T : MonoBehaviour, IPoolable
    {
        if (!_pools.ContainsKey(key))
        {
            IObjectPool<IPoolable> pool = new ObjectPool<T>(prefab, poolSize, transform);
            _pools.Add(key, pool);
        }
    }

    public IPoolable GetObject(string key)
    {
        if (_pools.ContainsKey(key))
        {
            return _pools[key].Get();
        }
        return null;
    }

    public void ReturnObject(string key, IPoolable obj)
    {
        if (_pools.ContainsKey(key))
        {
            _pools[key].Return(obj);
        }
    }
}
