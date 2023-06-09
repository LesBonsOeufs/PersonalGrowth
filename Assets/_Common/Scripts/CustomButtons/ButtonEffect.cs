///-----------------------------------------------------------------
///   Author :                     
///   Date   : 11/09/2022 22:57
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

namespace Com.GabrielBernabeu.Common.CustomButtons
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
    {
        [Header("Audio")]
        [SerializeField] private AudioClip audioSelected = default;
        [SerializeField] private AudioClip audioClick = default;
        [Header("Values OnPointerEnter")]
        [SerializeField] private float scaleTarget = 1.4f;
        [SerializeField] private float durationOnPoinertEnter = 0.5f;
        [Header("Values OnPointerExit")]
        [SerializeField] private float durationOnPointerExit = 0.5f;
        [Header("Values OnPointerClick")]
        [SerializeField] private float durationOnPointerClick = 0.5f;
        [Header("Values OnPointerDown")]
        [SerializeField] private float scaleDownTarget = 0.7f;
        [SerializeField] private float durationOnPoinertDown = 0.5f;
        [Header("Values Infinite Idle Breathing")]
        [SerializeField] private bool isBreathing = false;
        [SerializeField] private float scaleIdleTarget = 1.2f;
        [SerializeField] private float duration = 0.5f;
        [Space]
        [SerializeField] private UnityEvent _onClick = default;

        private AudioSource audioSource;
        private Vector3 initScale;

        public UnityEvent OnClick => _onClick;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            initScale = transform.localScale;
        }

        private void OnEnable()
        {
            if (isBreathing) InfiniteIdleBreathing();
        }

        private void OnButtonHighlighted()
        {
            audioSource.clip = audioSelected;
            audioSource.Play();
        }

        private void OnButtonPressed()
        {
            audioSource.clip = audioClick;
            audioSource.Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnButtonHighlighted();
            DOTween.Complete(transform);
            DOTween.Kill(transform);
            transform.DOScale(initScale * scaleTarget, durationOnPoinertEnter).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DOTween.Kill(transform);

            if (!isBreathing)
                transform.DOScale(initScale, durationOnPointerExit).SetUpdate(true);
            else
            {
                transform.DOScale(initScale, durationOnPointerExit).SetUpdate(true).OnComplete(InfiniteIdleBreathing);
            }    
        }

        public void OnPointerClick(PointerEventData eventData)
        {
#if !UNITY_ANDROID
            if (!Input.GetMouseButtonUp(0))
                return;
#endif

            DOTween.Complete(transform);
            DOTween.Kill(transform);

            transform.DOScale(initScale, durationOnPointerClick).SetUpdate(true);

            OnClick?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
#if !UNITY_ANDROID
            if (!Input.GetMouseButtonDown(0))
                return;
#endif

            OnButtonPressed();
            DOTween.Complete(transform);
            DOTween.Kill(transform);
            transform.DOScale(initScale * scaleDownTarget, durationOnPoinertDown).SetUpdate(true);
        }

        private void InfiniteIdleBreathing()
        {
            DOTween.Sequence(transform).SetLoops(-1)
                .Append(transform.DOScale(scaleIdleTarget, duration).SetEase(Ease.InOutSine))
                .Append(transform.DOScale(initScale, duration).SetEase(Ease.InOutSine)).SetUpdate(true);
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }
    }
}