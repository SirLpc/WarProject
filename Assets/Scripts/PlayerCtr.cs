using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using PathologicalGames;

public class PlayerCtr : MonoBehaviour
{
    #region ===字段===

    private TroopType _troopType;

    private PlayerMove _moveInstance;
    private PlayerFight _fightInstance;
    private PlayerAnt _antInstance;


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

    public PlayerAnt AntInstance
    {
        get { return _antInstance ?? (_antInstance = GetComponent<PlayerAnt>()); }
    }

    public TroopType CurTroopType
    {
        get { return _troopType; }
    }

    public int AntNum
    {
        get { return AntInstance.AntNum; }
    }

    public int HP { private set; get; }

    #endregion

    #region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

    #endregion

    #region ===方法===

    public void InitTroopAs(TroopType type, int number)
    {
        HP = number * 10;
        _troopType = type;

        AntInstance.InitAnts(number);
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
            AntInstance.DespawnAntsWhenDamage();
        }
    }

   
    #endregion
}
