using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour,
    IPointerDownHandler,
	IPointerUpHandler,
	IPointerClickHandler,
	IBeginDragHandler,
	IDragHandler,
	IEndDragHandler
{
	[Header("Interactions")]
	[ReadOnly, SerializeField] private bool dragClickL = false;
	[ReadOnly, SerializeField] private bool dragClickR = false;

	[Label("Pointer Down")]
	[Foldout("Left")] public UnityEvent onPointerDownL = default;
	[Foldout("Right")] public UnityEvent onPointerDownR = default;

    [Label("Pointer Up")]
    [Foldout("Left")] public UnityEvent onPointerUpL = default;
    [Foldout("Right")] public UnityEvent onPointerUpR = default;

    [Label("Click")]
	[Foldout("Left Click")] public UnityEvent onClickL = default;
	[Foldout("Right Click")] public UnityEvent onClickR = default;
	[Header("Double Click")]
	[Foldout("Left Click")] public UnityEvent onDoubleClickL = default;
	[Foldout("Right Click")] public UnityEvent onDoubleClickR = default;

	[Label("Drag")]
	[Foldout("Left Drag")] public UnityEvent onBeginDragL = default;
	[Foldout("Left Drag")] public UnityEvent onDuringDragL = default;
	[Foldout("Left Drag")] public UnityEvent onFinishDragL = default;

	[Foldout("Right Drag")] public UnityEvent onBeginDragR = default;
	[Foldout("Right Drag")] public UnityEvent onDuringDragR = default;
	[Foldout("Right Drag")] public UnityEvent onFinishDragR = default;

	public void OnPointerDown(PointerEventData eventData)
	{
		switch (eventData.button)
		{
			case PointerEventData.InputButton.Left:
				if (!dragClickL)
				{
					onPointerDownL?.Invoke();
                }
				break;
			case PointerEventData.InputButton.Right:
				if (!dragClickR)
				{
					onPointerDownR?.Invoke();
				}
				break;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                if (!dragClickL)
                {
					onPointerUpL?.Invoke();
                }
                break;
            case PointerEventData.InputButton.Right:
                if (!dragClickR)
                {
                    onPointerUpR?.Invoke();
                }
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
	{
		switch (eventData.button)
		{
			case PointerEventData.InputButton.Left:
				if (!dragClickL)
				{
					switch (eventData.clickCount)
					{
						case 1:
							onClickL?.Invoke();
							break;
						case 2:
							onDoubleClickL?.Invoke();
							break;
						default: break;
					}
				}
				break;
			case PointerEventData.InputButton.Right:
				if (!dragClickR)
				{
					switch (eventData.clickCount)
					{
						case 1:
							onClickR?.Invoke();
							break;
						case 2:
							onDoubleClickR?.Invoke();
							break;
						default: break;
					}
				}
				break;
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		switch (eventData.button)
		{
			case PointerEventData.InputButton.Left:
				dragClickL = true;
				onBeginDragL?.Invoke();
				break;
			case PointerEventData.InputButton.Right:
				dragClickR = true;
				onBeginDragR?.Invoke();
				break;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		switch (eventData.button)
		{
			case PointerEventData.InputButton.Left:
				if (dragClickL)
					onDuringDragL?.Invoke();
				break;
			case PointerEventData.InputButton.Right:
				if (dragClickR)
					onDuringDragR?.Invoke();
				break;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		switch (eventData.button)
		{
			case PointerEventData.InputButton.Left:
				dragClickL = false;
				onFinishDragL?.Invoke();
				break;
			case PointerEventData.InputButton.Right:
				dragClickR = false;
				onFinishDragR?.Invoke();
				break;
		}
	}
}