using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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

	public Dictionary<string, Context> _contextDic = new Dictionary<string, Context>();

	public Context GetContext(string name)
	{
		if (_contextDic.ContainsKey(name))
			return _contextDic[name];
		return null;
	}
}
