using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;

namespace DajiaGame.Px
{
    /// <summary>
    /// 描述：
    /// author： 
    /// </summary>
	[AddComponentMenu("DajiaGame/Px/ ZhanPanCtr ")]
    public class ZhanPanCtr : MonoBehaviour
    {
		#region ===字段===
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _body;
        #endregion

		#region ===属性===
	    #endregion

		#region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

        private void Start()
        {
            Rotate();
        }
		#endregion

		#region ===方法===

        private void Rotate()
        {
            DOTween.To(() => 0f, z => { _body.MoveRotation(z); }, 360f, 360f/_speed)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }

	    #endregion
    }
}