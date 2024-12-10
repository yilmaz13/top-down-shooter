using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool<IPoolable> where T : MonoBehaviour, IPoolable
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Stack<IPoolable> _pool;

    public ObjectPool(T prefab, int initialSize, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new Stack<IPoolable>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public IPoolable Get()
    {
        if (_pool.Count > 0)
        {
            IPoolable obj = _pool.Pop();
            obj.SetActive(true);
            obj.OnSpawn();
            return obj;
        }
        else
        {
            T obj = GameObject.Instantiate(_prefab, _parent);
            obj.OnSpawn();
            return obj;
        }
    }

    public void Return(IPoolable obj)
    {
        obj.OnDespawn();
        obj.SetActive(false);
        _pool.Push(obj);
    }
}
