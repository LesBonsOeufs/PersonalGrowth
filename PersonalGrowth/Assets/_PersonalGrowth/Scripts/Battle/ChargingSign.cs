using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    [RequireComponent(typeof(Image))]
    public class ChargingSign : MonoBehaviour
    {
        [SerializedDictionary, SerializeField] private SerializedDictionary<EChargeStatus, StatusInfo> statuses;
        [SerializedDictionary, SerializeField] private float statusTweenDuration = 0.4f;

        private Image image;
        private Tween effectTween;

        public EChargeStatus Status
        {
            get
            {
                return _status;
            }

            set
            {
                if (_status == value)
                    return;

                _status = value;
                effectTween?.Kill();
                StatusInfo statusInfo = statuses[_status];

                effectTween = DOTween.Sequence(transform)
                    .Append(image.DOColor(statusInfo.color, statusTweenDuration))
                    .Join(transform.DOScale(statusInfo.size, statusTweenDuration));
            }
        }
        private EChargeStatus _status;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void SetValue(float value01)
        {
            image.fillAmount = value01;
        }

        [Serializable]
        private struct StatusInfo
        {
            public Color color;
            public float size;
        }
    }

    public enum EChargeStatus
    {
        GROW,
        SHRINK,
        FULL,
        BLOCKED
    }
}