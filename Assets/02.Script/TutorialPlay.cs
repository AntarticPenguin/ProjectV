using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPlay : MonoBehaviour
{
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

	bool _isPlaying;

	string _curEventName;
	eEventState _eventState;

	Dictionary<string, TutorialScript>.Enumerator _enumerator;

	void Start()
    {
		_isPlaying = true;
		_eventState = eEventState.NONE;
		_enumerator = ResourceManager.Instance.GetScriptMap().GetEnumerator();

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
			}
		}
		else
		{
			_isPlaying = false;
			GameManager.Instance._editModeInput.OnBuildTileCallback -= BuildTileSuccess;
		}
	}

	void InitEvent()
	{
		_cameraLastPos = Camera.main.transform.position;
		_bBuildTileSuccess = false;

		GameManager.Instance._editModeInput.OnBuildTileCallback += BuildTileSuccess;
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
				result = EventBuileTile();
				break;
			case "remove_fog":
				result = EventRemoveFog();
				break;
			case "use_diamond":
				break;
			case "end_tutorial":
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

	bool _bBuildTileSuccess;
	bool EventBuileTile()
	{
		return _bBuildTileSuccess;
	}

	void BuildTileSuccess()
	{
		_bBuildTileSuccess = true;
	}

	bool EventRemoveFog()
	{
		return false;
	}
}
