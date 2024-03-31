using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
	public AudioClip clipSpecific;

	public void OnPointerDown(PointerEventData eventData)
	{
		if(clipSpecific != null) { AudioManager.instance.PlaySFX(clipSpecific); }
		else AudioManager.instance.PlayUISelectEvent();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		AudioManager.instance.PlayUIHoverEvent();
	}

}