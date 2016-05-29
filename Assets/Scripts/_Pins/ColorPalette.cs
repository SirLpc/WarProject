using UnityEngine;
using System.Collections.Generic;

namespace DajiaGame.Px
{
    /// <summary>
    /// 描述：
    /// author： 
    /// </summary>
	[AddComponentMenu("DajiaGame/Px/ ColorPalette ")]
    public class ColorPalette : MonoBehaviour
    {
		#region ===字段===

        public static ColorPalette Instance = null;

        public Color[] COLOR;

        #endregion

        #region ===属性===

        #endregion

        #region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        #endregion

        #region ===方法===

        #endregion
    }
}