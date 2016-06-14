using UnityEngine;
using System.Collections.Generic;

public class PlayerCtr : MonoBehaviour
{
	#region ===字段===

    private int _antNum;

    private GameObject _antPrefab;

	#endregion

	#region ===属性===
	#endregion

	#region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

    private void Awake()
    {
        _antPrefab = Resources.Load<GameObject>("AntUnit");
    }

    private void Start()
    {
        
    }

	#endregion

	#region ===方法===

	#endregion
}
