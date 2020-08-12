using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour, IInteractive
{
	public float _removeTime;
	public int _cost;

	public void Interact()
	{
		UIManager.Instance.ShowMessageBox("안개를 제거하시겠습니까?");
	}
}
