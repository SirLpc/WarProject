using UnityEngine;
using System.Collections.Generic;
using System.Net.Mime;

public class PlayerFight : MonoBehaviour
{
	#region ===字段===
	#endregion

	#region ===属性===
	#endregion

	#region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

    private void OnCollisionEnter(Collision collision)
    {
        Direction dir = Utils.GetDirection(transform, collision.transform);
        Debug.Log(transform.parent.name + "===" + dir.ToString());

    }

    #endregion

    #region ===方法===
    #endregion
}
