using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DajiaGame.Px
{
    /// <summary>
    /// 描述：
    /// author： 
    /// </summary>
	[AddComponentMenu("DajiaGame/Px/ GameCtr ")]
    public class GameCtr : MonoBehaviour
    {
		#region ===字段===

        public static GameCtr Instance = null;

        [SerializeField] private GameObject _pinPrefab;

        private Queue<PinCtr> _queueOfPins; 

	    #endregion

		#region ===属性===

        public bool IsReadyFire { get; set; }

	    #endregion

		#region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

        private void Awake()
        {
            Instance = this;
            _queueOfPins = new Queue<PinCtr>();
        }

        private void Start()
        {
            PrepareAPin();
            IsReadyFire = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireAPin();
            }    
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        #endregion

        #region ===方法===

        public void SettleFireResult(FIRER_RESULT fr)
        {
            Debug.Log(fr.ToString());
            switch (fr)
            {
                case FIRER_RESULT.GAP:
                    break;
                case FIRER_RESULT.NORMAL:
                    break;
                case FIRER_RESULT.OUT:
                    break;
            }
        }

        private void PrepareAPin()
        {
            GameObject go = Instantiate(_pinPrefab);
            var _curPin = go.GetComponent<PinCtr>();
            _curPin.InitPin(Random.Range(0, ColorPalette.Instance.COLOR.Length));
            _queueOfPins.Enqueue(_curPin);
        }

        private void FireAPin()
        {
            if (!IsReadyFire)
                return;
            IsReadyFire = false;

            _queueOfPins.Dequeue().Fire();
            PrepareAPin();
        }

        #endregion
    }
}