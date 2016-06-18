using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PoolTotleMgr : MonoBehaviour
{
    [SerializeField]
    private string[] _prefabPaths;

    public static PoolTotleMgr Instance = null;

	private SpawnPool spawnPool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        spawnPool = PoolManager.Pools["Troops"];
        //InitPool();
    }

    public Transform Spawn(string prefabName)
    {
        return spawnPool.Spawn(prefabName);
    }

    public void Despawn(Transform pref)
    {
        spawnPool.Despawn(pref);
    }

    public void ClearPool()
    {
        spawnPool.DespawnAll();
    }

    private void InitPool()
    {
        for (int i = 0; i < 2; i++)
        {
            Transform tr = Resources.Load<Transform>(_prefabPaths[i]);
            PrefabPool refabPool = new PrefabPool(tr);
            //默认初始化两个Prefab
            refabPool.preloadAmount = 2;
            //开启限制
            refabPool.limitInstances = false;
            //关闭无限取Prefab
            refabPool.limitFIFO = false;
            //限制池子里最大的Prefab数量
            refabPool.limitAmount = 5;
            //开启自动清理池子
            refabPool.cullDespawned = true;
            //最终保留
            refabPool.cullAbove = 10;
            //多久清理一次
            refabPool.cullDelay = 5;
            //每次清理几个
            refabPool.cullMaxPerPass = 5;
            //初始化内存池
            spawnPool._perPrefabPoolOptions.Add(refabPool);
            spawnPool.CreatePrefabPool(spawnPool._perPrefabPoolOptions[spawnPool.Count]);
        }
    }


}
