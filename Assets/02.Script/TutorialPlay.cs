using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialPlay : MonoBehaviour
{
	private static TutorialPlay _instance;

	public static TutorialPlay Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<TutorialPlay>();
				if(_instance == null)
				{
					GameObject go = new GameObject();
					go.name = "TutorialPlayInstance";
					go.AddComponent<TutorialPlay>();
				}
			}
			return _instance;
		}
	}

	enum eEventState
	{
		NONE,		//실행중인 이벤트 없음
		START,		//이벤트 시작
		PLAYING,	//퀘스트 진행중
		DONE,		//퀘스트 완료
	}

	public Canvas _tutorialCanvas;
	public GameObject _blockPanel;
	public GameObject _guideBox;
	public Text _guideText;
	public Button _skipButton;

	bool _isPlaying;

	string _curEventName;
	eEventState _eventState;

	Dictionary<string, TutorialScript>.Enumerator _enumerator;

	void Start()
    {
		_isPlaying = true;
		GameManager.Instance._isPlayingTutorial = _isPlaying;
		_eventState = eEventState.NONE;
		_enumerator = ResourceManager.Instance.GetScriptMap().GetEnumerator();

		_skipButton.onClick.AddListener(EndTutorial);

		//이벤트 관련 변수 초기화
		InitEvent();

		PlayNextScript();
	}

    void Update()
    {
		if (!_isPlaying)
			return;

		if(eEventState.NONE == _eventState && Input.GetMouseButtonDown(0))
		{
			PlayNextScript();
		}
		else if(eEventState.START == _eventState && Input.GetMouseButtonDown(0))
		{
			//튜토리얼 텍스트 창을 닫고 퀘스트를 시작한다.
			_guideBox.SetActive(false);
			_blockPanel.SetActive(false);
			_eventState = eEventState.PLAYING;
		}
		else if (eEventState.PLAYING == _eventState)
		{
			if(CheckEventState(_curEventName))
			{
				_eventState = eEventState.DONE;
				_curEventName = "empty";
			}
		}
		else if (eEventState.DONE == _eventState)
		{
			//퀘스트가 완료되면 가이드 박스를 활성화 시켜준다.
			_guideBox.SetActive(true);
			_blockPanel.SetActive(true);
			_eventState = eEventState.NONE;

			PlayNextScript();
		}
    }

	void PlayNextScript()
	{
		if (_enumerator.MoveNext())
		{
			TutorialScript script = _enumerator.Current.Value;
			_guideText.text = script.text;

			if (!script.eventName.Equals("empty"))
			{
				_curEventName = script.eventName;
				_eventState = eEventState.START;

				if(_curEventName == "build_tile")
				{
					GameManager.Instance._playerInput.OnEditMode();
				}
				else if(_curEventName == "remove_fog")
				{
					GameManager.Instance._playerInput.OnCameraMode();
				}
			}
		}
		else
		{
			EndTutorial();
		}
	}

	public bool _bBuildTileSuccess;
	public bool _bRemoveFogSuccess;

	void InitEvent()
	{
		_cameraLastPos = Camera.main.transform.position;
		_bBuildTileSuccess = false;
		_bRemoveFogSuccess = false;
	}

	bool CheckEventState(string name)
	{
		bool result = false;
		switch (name)
		{
			case "move_camera":
				result = EventMoveCamera();
				break;
			case "build_tile":
				result = _bBuildTileSuccess;
				break;
			case "remove_fog":
				result = _bRemoveFogSuccess;
				break;
			default:
				break;
		}

		return result;
	}

	Vector3 _cameraLastPos;
	float _moveDistance = 0.0f;
	bool EventMoveCamera()
	{
		float distance = Vector3.Distance(Camera.main.transform.position, _cameraLastPos);
		_moveDistance += distance;
		_cameraLastPos = Camera.main.transform.position;

		if (_moveDistance > 20.0f)
			return true;
		return false;
	}

	void EndTutorial()
	{
		//튜토리얼을 끝내고 새 월드를 연다.
		_isPlaying = false;
		GameManager.Instance._isPlayingTutorial = false;
		GameManager.Instance._tutorialComplete = true;

		StartCoroutine(LoadPlayerWorldAsync("PlayerWorld"));
	}

	IEnumerator LoadPlayerWorldAsync(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

		while(!operation.isDone)
		{
			yield return null;
		}
	}
}
