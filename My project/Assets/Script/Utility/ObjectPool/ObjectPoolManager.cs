using System;
using System.Collections;
using System.Collections.Generic;
using Script.Utility.ObjectPool;

public class ObjectPoolManager
{
    private static ObjectPoolManager _instance = null;
    public static ObjectPoolManager instance => _instance ??= new ObjectPoolManager();
    
    private Dictionary<string, ObjectPoolBase> _pools = new();

    private ObjectPoolManager()
    {
        
    }

    public ObjectPoolBase Register<T>(string name) where T : IObjectPoolElement, new()
    {
        if (_pools.TryGetValue(name, out var pool))
        {
            return pool;
        }
        var result = new ObjectPool<T>(name);
        _pools.TryAdd(name, result);
        return result;
    }

    public ObjectPoolBase Get(string name)
    {
        return _pools.GetValueOrDefault(name);
    }

    public void Deregister(string name)
    {
        var pool = Get(name);
        if (pool != null)
        {
            Deregister(pool);
        }
    }

    public void Deregister(ObjectPoolBase pool)
    {
        _pools.Remove(pool.name);
        pool.Destory();
    }
}
