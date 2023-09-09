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

        public float Value
        {
            get
            {
                return image.fillAmount;
            }

            set
            {
                image.fillAmount = value;
            }
        }

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