using UnityEngine;
using System.Collections.Generic;

namespace DajiaGame.Px
{
    /// <summary>
    /// 描述：
    /// author： 
    /// </summary>
	[AddComponentMenu("DajiaGame/Px/ PinCtr ")]
    public class PinCtr : MonoBehaviour
    {

        public struct PerfectAngle
        {
            public float Min;
            public float Max;

            public PerfectAngle(float min, float max)
            {
                Min = min;
                Max = max;
            }
        }

        #region ===字段===

        [SerializeField]
        private float _speed;

        [SerializeField] private Collider2D _bodyCollider;
        [SerializeField] private Collider2D _baseCollider;

        private Rigidbody2D _body;
        private SpriteRenderer[] _childRenderers;
        private PIN_TYPE _pinType;
        private PerfectAngle _perfectAngle;

        private const float PiceAngle = 51.4f;
        private const float HalfPiceAngle = 25.7f;
        private const float GapThreshold = 3.4f;
        private const float HalfGapThreshold = 1.7f;
        private const float HalfPiceAngleNOThreshold = HalfPiceAngle - HalfGapThreshold;

        /// <summary>
        /// Use property instead!
        /// </summary>
        private static readonly Dictionary<PIN_TYPE, PerfectAngle> _dicOfPinPerfectAngle = null;

        #endregion

        #region ===属性===

        public PIN_TYPE PinType
        {
            get { return _pinType; }
        }

        public static Dictionary<PIN_TYPE, PerfectAngle> DicOfPinPerfectAngle
        {
            get
            {
                if (_dicOfPinPerfectAngle != null)
                    return _dicOfPinPerfectAngle;

                Dictionary<PIN_TYPE, PerfectAngle> result = new Dictionary<PIN_TYPE, PerfectAngle>();
                for (int i = 0; i < Consts.ZhuanPanPice; ++i)
                    result.Add((PIN_TYPE)i, GetPerfectAngle(i));
                return result;
            }
        }

        public PerfectAngle ThePerfectAngle
        {
            get { return DicOfPinPerfectAngle[PinType]; }
        }

        #endregion

        #region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _childRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            ZhanPanCtr zp = collision.gameObject.GetComponent<ZhanPanCtr>();
            if (zp != null)
            {
                Stop();
                transform.SetParent(zp.transform);
                FIRER_RESULT fr = CheckFireResult(transform.eulerAngles.z, this);
                GameCtr.Instance.SettleFireResult(fr);
                return;
            }
            PinCtr pin = collision.gameObject.GetComponent<PinCtr>();
            if (pin != null)
            {
                Debug.Log("game over!");
            }
        }

        //public void OnTriggerEnter2D(Collider2D collision)
        //{
        //    PinCtr pin = collision.GetComponentInParent<PinCtr>();
        //    if (pin == null) return;

        //    Debug.Log("Game Over ! ");
        //}

        #endregion

        #region ===方法===

        public void InitPin(int index)
        {
            _pinType = (PIN_TYPE)index;

            if (_childRenderers == null) return;
            foreach (var item in _childRenderers)
            {
                item.color = ColorPalette.Instance.COLOR[index];
            }
            transform.position = Vector3.zero;
        }

        public void Fire()
        {
            _body.AddForce(Vector2.up * _speed);
        }

        public void Stop()
        {
            _bodyCollider.enabled = false;
            _body.isKinematic = true;
        }

        #endregion

        #region ===静态方法===

        //public static bool IsInPiceAngle(float angle, PinCtr pin)
        //{
        //    if (pin.IsCounted) return false;
        //    pin.IsCounted = true;

        //    var perfectAngle = pin.ThePerfectAngle;

        //    if ((int)pin.PinType != 0)
        //        return angle > perfectAngle.Min && angle < perfectAngle.Max;

        //    return angle < perfectAngle.Min || angle > perfectAngle.Max;
        //}

        public static FIRER_RESULT CheckFireResult(float angle, PinCtr pin)
        {
            FIRER_RESULT result = FIRER_RESULT.GAP;
            for (int i = 0; i < Consts.ZhuanPanPice; i++)
            {
                PIN_TYPE pt = (PIN_TYPE)i;
                if (!IsInPiceAngle(angle, pt)) continue;
                result = pin.PinType != pt ? FIRER_RESULT.OUT : FIRER_RESULT.NORMAL;
                break;
            }
            return result;
        }

        public static bool IsInPiceAngle(float angle, PIN_TYPE type)
        {
            var perfectAngle = DicOfPinPerfectAngle[type];

            if ((int)type != 0)
                return angle > perfectAngle.Min && angle < perfectAngle.Max;

            return angle < perfectAngle.Min || angle > perfectAngle.Max;
        }

        private static PerfectAngle GetPerfectAngle(int index)
        {
            float min, max;
            if (index == 0)
            {
                max = 360 + HalfPiceAngleNOThreshold * -1;
                min = HalfPiceAngleNOThreshold;
            }
            else
            {
                index = 7 - index;
                min = (index * 2 - 1) * HalfPiceAngleNOThreshold + GapThreshold * index;
                max = min + HalfPiceAngleNOThreshold * 2;
            }
            return new PerfectAngle(min, max);
        }

        #endregion
    }
}