using Com.GabrielBernabeu.PersonalGrowth.Battle;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TouchOverlay : MonoBehaviour
{
    [SerializeField] private ShowHideTweener showHideTweener;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            showHideTweener.Show();
        else if (Input.GetMouseButtonUp(0))
            showHideTweener.Hide();

        if (showHideTweener.IsShown)
            MoveToMouse();
    }

    private void MoveToMouse()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform, Input.mousePosition, Camera.main, out Vector3 lCanvasMousePosition);

        transform.position = lCanvasMousePosition;
    }
}