using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TouchOverlay : MonoBehaviour
{
    [SerializeField] private float showDuration = 0.5f;
    [SerializeField] private float hideDuration = 0.5f;

    private RectTransform rectTransform;
    private Tween showTween;
    private bool isShown = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Show();
        else if (Input.GetMouseButtonUp(0))
            Hide();

        if (isShown)
            MoveToMouse();
    }

    private void Show()
    {
        isShown = true;
        showTween?.Kill();
        showTween = transform.DOScale(1f, showDuration).SetEase(Ease.OutBack);
    }

    private void Hide()
    {
        isShown = false;
        showTween?.Kill();
        showTween = transform.DOScale(0f, hideDuration).SetEase(Ease.InSine);
    }

    private void MoveToMouse()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle
            (rectTransform, Input.mousePosition, Camera.main, out Vector3 lCanvasMousePosition);

        transform.position = lCanvasMousePosition;
    }
}