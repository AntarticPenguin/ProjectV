using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour, IInteractive
{
	public float _removeTime;
	public int _cost;

	public void Interact()
	{
		UIManager.Instance.ShowMessageBox("안개를 제거하시겠습니까?", RemoveFog);
	}

	void RemoveFog()
	{
		//Ok버튼 콜백처리
		if(CurrencySystem.Instance.UseGold(_cost))
		{
			UIManager.Instance.CreateTimeUI(gameObject, _removeTime);        //남은시간 UI 표시
		}
		else
		{
			Debug.Log("골드가 부족합니다.");
		}
	}
}
