using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

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
        public ScoreChangeEvent OnScoreChanged;

        [SerializeField]
        private GameObject _pinPrefab;
        [SerializeField]
        private ZhanPanCtr _zhanPanCtr;

        private Queue<PinCtr> _queueOfPins;
        private int _score;

	    #endregion

		#region ===属性===

        public bool IsReadyFire { get; set; }

        public int Score {
            get { return _score; }
            private set {
                _score = value;
                OnScoreChanged.Invoke(value);
            }
        }

        #endregion

        #region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

        private void Awake()
        {
            Instance = this;
            _queueOfPins = new Queue<PinCtr>();
            OnScoreChanged = new ScoreChangeEvent();
        }

        private void Start()
        {
            Score = 0;
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
            var score = 0;
            switch (fr)
            {
                case FIRER_RESULT.GAP:
                    score = 30;
                    break;
                case FIRER_RESULT.NORMAL:
                    score = 20;
                    break;
                case FIRER_RESULT.OUT:
                    score = 10;
                    break;
            }
            Score += score;
        }

        public void OverGame()
        {
            _zhanPanCtr.Stop();
            IsReadyFire = false;
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
            StartCoroutine(CoReadyNextPin());
        }

        private IEnumerator CoReadyNextPin()
        {
            yield return null;
            PrepareAPin();

        }

        #endregion
    }

    public class ScoreChangeEvent : UnityEvent<int> { }
}