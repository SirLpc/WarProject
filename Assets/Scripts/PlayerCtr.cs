using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using PathologicalGames;

public class PlayerCtr : MonoBehaviour
{
    #region ===字段===
    [SerializeField]
    private Transform _antContainer;

    private int _antNum;
    private TroopType _troopType;
    private BoxCollider _collider;

    private PlayerMove _moveInstance;
    private PlayerFight _fightInstance;
    private Dictionary<Vector2, Transform> _ants; 

	#endregion

	#region ===属性===

    public PlayerMove MoveInstance
    {
        get { return _moveInstance ?? (_moveInstance = GetComponentInChildren<PlayerMove>()); }
    }

    public PlayerFight FightInstance
    {
        get { return _fightInstance ?? (_fightInstance = GetComponentInChildren<PlayerFight>()); }
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

    public int AntNum
    {
        get { return _antNum; }
    }

    public int HP { private set; get; }

    #endregion

    #region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

    private void Awake()
    {
        _collider = GetComponentInChildren<BoxCollider>();
    }

    #endregion

    #region ===方法===

    public void InitTroopAs(TroopType type, int number)
    {
        _antNum = number;
        HP = _antNum*10;
        _troopType = type;
        Ants.Clear();
        SpawnShap();

        MoveInstance.Init();
        FightInstance.Init();
    }
  
    public void TakeDamage(int damage)
    {
        HP -= damage;

        Debug.Log(gameObject.name + "take damage" + damage + "left" + HP);

        if (HP <= 0)
        {
            HP = 0;
            FightInstance.StopAttack();
            PoolTotleMgr.Instance.Despawn(transform);
        }
        else
        {
            while (Ants.Count - HP / 10 >= 1)
            {
                int lastIndex = Ants.Count - 1;
                //PoolTotleMgr.Instance.Despawn(Ants[lastIndex]);
                //Ants.RemoveAt(lastIndex);
            }
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
                Transform tr = PoolTotleMgr.Instance.Spawn(Consts.UnitPrefName[(int)_troopType]);
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

    #endregion
}
