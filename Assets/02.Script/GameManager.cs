using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	public static GameManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<GameManager>();
				if(_instance == null)
				{
					GameObject go = new GameObject();
					_instance = go.AddComponent<GameManager>();
					go.name = "GameManager";
				}
			}

			return _instance;
		}
	}

	private void Awake()
	{
		_mainCamera = GameObject.FindGameObjectWithTag("MainVirtualCamera").GetComponent<CinemachineVirtualCamera>();
		DontDestroyOnLoad(gameObject);
	}

	public EditModeInput _editModeInput;
	public InteractiveModeInput _interactiveModeInput;
	private CinemachineVirtualCamera _mainCamera;

	public CinemachineVirtualCamera GetMainCamera() { return _mainCamera; }
}
