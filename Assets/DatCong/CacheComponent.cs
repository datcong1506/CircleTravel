using System.Collections.Generic;
using UnityEngine;

public class CacheComponent<T>
{
    private Dictionary<GameObject, T> cache;

    public CacheComponent()
    {
        cache = new Dictionary<GameObject, T>();
    }

    public T Get(GameObject from)
    {
        if (!cache.ContainsKey(from))
        {
            cache.Add(from,from.GetComponent<T>());
        }

        return cache[from];
    }

    public bool Contain(GameObject key)
    {
        return cache.ContainsKey(key);
    }
    public bool TryGet(GameObject from, out T t)
    {
        if (!cache.ContainsKey(from))
        {
            t = default(T);
            return false;
        }

        t = cache[from];
        return true;
    }

    public bool Add(GameObject from)
    {
        if (!cache.ContainsKey(from))
        {
            cache.Add(from,from.GetComponent<T>());
            return true;
        }
        return false;
    }

    public bool Remove(GameObject from)
    {
        if (cache.ContainsKey(from))
        {
            cache.Remove(from);
            return true;
        }
        return false;
    }

    public void Clear()
    {
        cache.Clear();
    }
}