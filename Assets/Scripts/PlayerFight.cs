using UnityEngine;
using System.Collections.Generic;
using System.Net.Mime;

public class PlayerFight : MonoBehaviour
{
	#region ===字段===

    private PlayerCtr _ctr;

    private List<PlayerCtr> _enemyCtrs;

	#endregion

	#region ===属性===

    public PlayerCtr Ctr
    {
        get { return _ctr ?? (GetComponentInParent<PlayerCtr>()); }
    }

    public int ATK
    {
        get { return Ctr.AntNum; }
    }

    public List<PlayerCtr> EnemyCtrs
    {
        get { return _enemyCtrs ?? (_enemyCtrs = new List<PlayerCtr>()); }
    } 

    private bool InWar { get; set; }
    #endregion

	#region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

    private void OnCollisionEnter(Collision collision)
    {
        Transform tr = collision.transform;
        PlayerCtr pc = tr.GetComponentInParent<PlayerCtr>();
        if (pc != null)
        {
            Direction dir = Utils.GetDirection(transform, tr);
            Debug.Log(transform.parent.name + "===" + dir.ToString());
            Ctr.MoveInstance.PauseMove(true);

            EnemyCtrs.Add(pc);
            if(!InWar)
                InvokeRepeating("Attack", 0f, 1f);
        }
    }

    #endregion

    #region ===方法===

    public void Init()
    {
        EnemyCtrs.Clear();
        InWar = false;
    }

    public void StopAttack()
    {
        Debug.Log("attack stopped!");
        CancelInvoke("Attack");
        InWar = false;
    }

    private void Attack()
    {
        int enemyNum = EnemyCtrs.Count;
        if (enemyNum > 0)
        {
            for (int i = 0; i < EnemyCtrs.Count; i++)
            {
                PlayerCtr item = EnemyCtrs[i];
                if (item.gameObject.activeSelf)
                    item.TakeDamage(ATK);
                if (!item.gameObject.activeSelf)
                    EnemyCtrs.Remove(item);
            }
        }
        else
        {
            StopAttack();
            Ctr.MoveInstance.PauseMove(false);
        }
    }


    #endregion
}
