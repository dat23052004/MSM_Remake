using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class SimplePool
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();

    // Khoi tao pool moi
    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS EMTY !!! ");
            return;
        }

        if (!poolInstance.ContainsKey(prefab.PoolType) || poolInstance[prefab.PoolType] == null)
        {
            Pool p = new Pool();
            p.Preload(prefab, amount, parent);
            poolInstance[prefab.PoolType] = p;
        }
    }

    // Lay phan tu ra
    public static T Spawn<T>(PoolType poolType, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD !!! ");
            return null;
        }

        return poolInstance[poolType].Spawn(pos, rot) as T;
    }

    // tra phan tu vao
    public static void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.PoolType))
        {
            Debug.LogError(unit.PoolType + "IS NOT PRELOAD !!! ");
        }
        poolInstance[unit.PoolType].Despawn(unit);
    }

    // Thu thap phan tu
    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD !!! ");
        }
        poolInstance[poolType].Collect();
    }

    // thu thap tat ca
    public static void CollectAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    // Destroy 1 pool 
    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD !!! ");
        }
        poolInstance[poolType].Release();
    }

    // Destroy tat ca
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}

public class Pool
{
    Transform parent;   // noi luu tru pool
    GameUnit prefabs;
    Queue<GameUnit> inactives = new Queue<GameUnit>();  // chua cac unit dang o trong pool
    List<GameUnit> actives = new List<GameUnit>();   // cac unit dang duoc su dung

    // Khoi tao pool
    public void Preload(GameUnit prefabs, int mount, Transform parent)
    {
        this.parent = parent;
        this.prefabs = prefabs;
        for (int i = 0; i < mount; i++)
        {
            Despawn(Spawn(UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity));
        }
    }

    //Lay phan ra tu pool
    public GameUnit Spawn(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot)
    {
        GameUnit unit;
        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefabs, parent);
        }
        else
        {
            unit = inactives.Dequeue();
        }
        unit.TF.SetPositionAndRotation(pos, rot);
        //unit.TF.position = pos;
        //unit.TF.rotation = rot;
        actives.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
    }

    // tra phan tu ve pool
    public void Despawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }

    // dua tat ca phan tu dnag dung ve pool
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }

    // destroy tat ca phan tu
    public void Release()
    {
        Collect();
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }
        inactives.Clear();
    }
}