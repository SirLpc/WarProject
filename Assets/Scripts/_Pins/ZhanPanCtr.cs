﻿using UnityEngine;
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
        [SerializeField]
        private float _speed;

        private Rigidbody2D _body;
        private Tweener _zhanPanTweener = null;
        #endregion

        #region ===属性===

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        #endregion

        #region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

        private void Start()
        {
            Rotate();
        }

        private void OnDestroy()
        {
            if (_zhanPanTweener != null)
                _zhanPanTweener.Kill();
        }

        #endregion

        #region ===方法===

        public void Stop()
        {
            if (_zhanPanTweener != null)
                _zhanPanTweener.Pause();
        }

        private void Rotate()
        {
            _zhanPanTweener = DOTween.To(() => 0f, z => { _body.MoveRotation(z); }, 360f, 360f / _speed)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }

        #endregion
    }
}