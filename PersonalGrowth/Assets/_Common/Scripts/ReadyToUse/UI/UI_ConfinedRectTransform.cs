using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UI_ConfinedRectTransform : MonoBehaviour
{
    [SerializeField] private RectTransform confiner;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private Vector2 ClampPosition(Rect confinerRect)
    {
        Vector2 lSize = rectTransform.sizeDelta;
        Vector2 lAnchorFromCenter = rectTransform.anchorMax - Vector2.one * 0.5f;
        Vector2 lConfinerSize = confinerRect.size;

        // Calculate the needed offset from the pivot & the anchors' position
        Vector2 lOffset = new Vector2(
                lSize.x * rectTransform.pivot.x - lConfinerSize.x * lAnchorFromCenter.x, 
                lSize.y * rectTransform.pivot.y - lConfinerSize.y * lAnchorFromCenter.y
            );

        float lMinX = confinerRect.xMin + lOffset.x;
        float lMaxX = confinerRect.xMax + lOffset.x - lSize.x;
        float lMinY = confinerRect.yMin + lOffset.y;
        float lMaxY = confinerRect.yMax + lOffset.y - lSize.y;

        Vector2 lPosition = rectTransform.anchoredPosition;
        lPosition.x = Mathf.Clamp(lPosition.x, lMinX, lMaxX);
        lPosition.y = Mathf.Clamp(lPosition.y, lMinY, lMaxY);

        return lPosition;
    }

    private void LateUpdate()
    {
        rectTransform.anchoredPosition = ClampPosition(confiner.rect);
    }
}