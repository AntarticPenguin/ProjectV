using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCurrencyType
{
	GOLD,
	TICKET,		//즉시 완료 티켓
}

public class CurrencySystem : MonoBehaviour
{
	private static CurrencySystem _instance;

	public static CurrencySystem Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<CurrencySystem>();
				if(_instance == null)
				{
					GameObject go = new GameObject();
					go.name = "CurrencySystem";
					_instance = go.AddComponent<CurrencySystem>();
				}
			}

			return _instance;
		}
	}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	private int _remainGold;
	private int _remainTicket;

	public delegate void GetGoldDelegate();
	public GetGoldDelegate OnGetGoldCallback;

	public delegate void GetTicketDelegate();
	public GetTicketDelegate OnGetTicketCallback;

	public int GetRemainGold()
	{
		return _remainGold;
	}

	public int GetRemainTicket()
	{
		return _remainTicket;
	}

	public void AddGold(int amount)
	{
		_remainGold += amount;

		OnGetGoldCallback?.Invoke();
	}

	public void AddTicket(int amount)
	{
		_remainTicket += amount;
		OnGetTicketCallback?.Invoke();
	}
}
