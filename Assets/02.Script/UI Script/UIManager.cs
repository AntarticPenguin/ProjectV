using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
	private static UIManager _instance;

	public static UIManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<UIManager>();
				if (_instance == null)
				{
					GameObject go = new GameObject();
					_instance = go.AddComponent<UIManager>();
					go.name = "UIManager";
				}
			}

			return _instance;
		}
	}

	public GameObject _hudCanvas;
	public Dictionary<string, Context> _contextDic = new Dictionary<string, Context>();

	public Context GetContext(string name)
	{
		if (_contextDic.ContainsKey(name))
			return _contextDic[name];
		return null;
	}

	public void ShowMessageBox(string text, UnityAction okCallback = null)
	{
		GameObject prefab = ResourceManager.Instance.GetUIPrefab("MessageBox");

		if(prefab)
		{
			GameObject go = Instantiate(prefab);
			go.transform.SetParent(_hudCanvas.transform);
			go.transform.localPosition = Vector3.zero;
			
			MessageBox msgbox = go.GetComponent<MessageBox>();
			msgbox.contentText.text = text;

			if(okCallback != null)
				msgbox.OnOkCallback += okCallback;
		}
	}


	public void CreateTimeUI(GameObject target, float removeTime)
	{
		GameObject prefab = ResourceManager.Instance.GetUIPrefab("TimeRemainSlider");

		if(prefab)
		{
			GameObject go = Instantiate(prefab);
			go.transform.SetParent(target.transform);
			go.transform.localPosition = Vector3.zero;

			TimeSlider timeSlider = go.GetComponentInChildren<TimeSlider>();
			timeSlider.SetTime(target, removeTime);
		}
	}
}
