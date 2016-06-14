using UnityEngine;
using System.Collections.Generic;

public class PlayerCtr : MonoBehaviour
{
    #region ===字段===
    [SerializeField]
    private Transform _antContainer;

    private int _antNum;

    private GameObject _antPrefab;
    private BoxCollider _collider;

	#endregion

	#region ===属性===
	#endregion

	#region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

    private void Awake()
    {
        _antPrefab = Resources.Load<GameObject>(Consts.AntUnitPrefPath);
        _collider = GetComponentInChildren<BoxCollider>();
    }

    private void Start()
    {
        //_antNum = Random.Range(9, 26);
        _antNum = 9;
        int sqrNum = (int)Mathf.Sqrt(_antNum);
        float size, tx, tz, nx;
        size = tx = tz = nx = 0f;
        for (int i = 0; i < sqrNum; i++)
        {
            for (int j = 0; j < sqrNum; j++)
            {
                Transform tr = Instantiate(_antPrefab).transform;
                if(i == 0)
                {
                    SpriteRenderer r = tr.GetComponent<SpriteRenderer>();
                    r.color = Color.red;
                    if(size == 0)
                    {
                        size = r.bounds.size.x;
                        tx = tz = nx = sqrNum * size / 2 + (-1) * size / 2;
                        Debug.Log(tx);
                    }
                }
                tr.SetParent(_antContainer);
                tr.localPosition = new Vector3(tx, 0, tz);
                tx -= size;
            }
            tz -= size;
            tx = nx;
        }
        float colSize = sqrNum * size;
        _collider.size = new Vector3(colSize, _collider.bounds.size.y, colSize);
    }

	#endregion

	#region ===方法===

	#endregion
}
