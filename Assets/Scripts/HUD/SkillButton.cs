using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private float RESIZE_SCALE = 1.1f;
	private RectTransform recT;

	void Start()
	{
		recT = GetComponent<RectTransform>();
	}

	//Do this when the cursor enters the rect area of this selectable UI object.
	public void OnPointerEnter(PointerEventData eventData)
	{
		recT.sizeDelta = new Vector2(RESIZE_SCALE, RESIZE_SCALE);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		recT.sizeDelta = new Vector2(RESIZE_SCALE, RESIZE_SCALE);
	}

}
