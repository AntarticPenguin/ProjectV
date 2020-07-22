using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentItem : MonoBehaviour, IPointerClickHandler
{
	public GameObject prefab;

	public void OnPointerClick(PointerEventData eventData)
	{
		//스프라이트가 클릭되면 해당되는 프리팹을
		//EditMode의 selectedPrefab으로 설정한다.
		EditModeInput inputMode = GameManager.Instance.playerInputMode as EditModeInput;
		inputMode.SetSelectedPrefab(prefab);
	}
}
