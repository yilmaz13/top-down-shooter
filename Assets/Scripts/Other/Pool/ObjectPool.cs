using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool<T> where T : MonoBehaviour, IPoolable
{
    private readonly Queue<T> _poolQueue = new Queue<T>();
    private readonly T _prefab;
    private readonly int _initialSize;
    private readonly Transform _parent;

    public ObjectPool(T prefab, int initialSize, Transform parent)
    {
        _prefab = prefab;
        _initialSize = initialSize;
        _parent = parent;

        InitializePool(parent);
    }

    private void InitializePool(Transform parent)
    {
        for (int i = 0; i < _initialSize; i++)
        {
            T obj = Object.Instantiate(_prefab);
            obj.gameObject.SetActive(false);  
            obj.transform.SetParent(parent);
            _poolQueue.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (_poolQueue.Count > 0)
        {
            T obj = _poolQueue.Dequeue();
            obj.gameObject.SetActive(true);
            obj.OnSpawn();
            return obj;
        }
        else
        {
            T obj = Object.Instantiate(_prefab);
            obj.OnSpawn();
            return obj;
        }
    }

    public void Return(T obj)
    {
        obj.OnDespawn();
        obj.gameObject.SetActive(false);
        _poolQueue.Enqueue(obj);
    }
}
