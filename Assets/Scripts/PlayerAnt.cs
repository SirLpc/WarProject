using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnt : MonoBehaviour
{
    [SerializeField]
    private Transform _antContainer;

    private PlayerCtr _ctr;
    private BoxCollider _collider;
    private Dictionary<Vector2, Transform> _ants;
    private int _antNum;

    public int AntNum
    {
        get { return _antNum; }
    }

    private PlayerCtr Ctr
    {
        get { return _ctr ?? (_ctr = GetComponent<PlayerCtr>()); }
    }

    /// <summary>
    /// 右上角为00，左下角为maxmax
    /// 02  01  00
    /// 12  11  10
    /// 22  21  20
    /// </summary>
    public Dictionary<Vector2, Transform> Ants
    {
        get { return _ants ?? (_ants = new Dictionary<Vector2, Transform>()); }
    }


    private void Awake()
    {
        _collider = GetComponentInChildren<BoxCollider>();
    }

    public void InitAnts(int number)
    {
        _antNum = number;
        Ants.Clear();
        SpawnShap();
    }

    public void DespawnAntsWhenDamage()
    {
        while (Ants.Count - Ctr.HP / 10 >= 1)
        {
            int lastIndex = Ants.Count - 1;
            var count = Ants.Keys.Count;
            Vector2[] keyArr = new Vector2[count];
            Ants.Keys.CopyTo(keyArr, 0);
            var temp = keyArr[count - 1];
            PoolTotleMgr.Instance.Despawn(Ants[temp]);
            Ants.Remove(temp);
        }
    }

    private void SpawnShap()
    {
        int sqrNum = Mathf.CeilToInt(Mathf.Sqrt(_antNum));
        float sizeX, sizeZ, tx, tz, nx, nz;
        sizeX = sizeZ = tx = tz = nx = nz = 0f;
        int row = 0;
        for (int i = 0; i < sqrNum; i++)
        {
            for (int j = 0; j < sqrNum; j++)
            {
                if (i * sqrNum + (j + 1) > _antNum)
                    break;
                Transform tr = PoolTotleMgr.Instance.Spawn(Consts.UnitPrefName[(int)Ctr.CurTroopType]);
                Ants.Add(new Vector2(i, j), tr);
                if (i == 0)
                {
                    SpriteRenderer r = tr.GetComponent<SpriteRenderer>();
                    r.color = Color.red;
                    if (sizeX == 0)
                    {
                        sizeX = r.bounds.size.x;
                        sizeZ = r.bounds.size.y;
                        tx = nx = sqrNum * sizeX / 2 + (-1) * sizeX / 2;
                        tz = nz = sqrNum * sizeZ / 2 + (-1) * sizeZ / 2;
                    }
                }
                tr.SetParent(_antContainer);
                tr.localPosition = new Vector3(tx, 0, tz);
                tr.Rotate(90, 0, 0);
                tx -= sizeX;
            }
            tz -= sizeZ;
            tx = nx;
        }
        float colSizeX = sqrNum * sizeX;
        int offset = sqrNum - Mathf.RoundToInt(Mathf.Sqrt(_antNum));
        float colSizeZ = colSizeX - offset * sizeX;
        _collider.size = new Vector3(colSizeX, _collider.bounds.size.y, colSizeZ);
        _collider.center += Vector3.forward * sizeX * offset / 2;


        //Ants[Vector2.zero].GetComponent<SpriteRenderer>().color = Color.blue;
        //Ants[new Vector2(0, 4)].GetComponent<SpriteRenderer>().color = Color.cyan;
    }

}
