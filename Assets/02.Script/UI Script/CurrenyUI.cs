using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrenyUI : MonoBehaviour
{
	public Text _textGold;
	public Text _textDiamond;

	// Start is called before the first frame update
	void Start()
    {
		CurrencySystem.Instance.OnCurrencyUpdateCallback += UpdateUI;
		UpdateUI();
	}

	void UpdateUI()
	{
		_textGold.text = CurrencySystem.Instance.GetRemainGold().ToString();
		_textDiamond.text = CurrencySystem.Instance.GetRemainDiamond().ToString();
	}
}