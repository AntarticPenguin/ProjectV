using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		_remainGold = 1000;
	}

	private int _remainGold;
	private int _remainDiamond;

	public delegate void CurrenyUpdateDelegate();
	public CurrenyUpdateDelegate OnCurrencyUpdateCallback;

	public int GetRemainGold()
	{
		return _remainGold;
	}

	public int GetRemainDiamond()
	{
		return _remainDiamond;
	}

	public void AddGold(int amount)
	{
		_remainGold += amount;

		OnCurrencyUpdateCallback?.Invoke();
	}

	public void AddDiamond(int amount)
	{
		_remainDiamond += amount;
		OnCurrencyUpdateCallback?.Invoke();
	}

	public bool UseGold(int cost)
	{
		if(cost <= _remainGold)
		{
			_remainGold -= cost;
			OnCurrencyUpdateCallback?.Invoke();
			return true;
		}
		return false;
	}

	public bool UseDiamond(int cost)
	{
		if (cost <= _remainDiamond)
		{
			_remainDiamond -= cost;
			OnCurrencyUpdateCallback?.Invoke();
			return true;
		}
		return false;
	}
}
