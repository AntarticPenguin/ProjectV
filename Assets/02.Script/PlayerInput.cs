using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
	private ICommand _cameraUp, _cameraDown, _cameraLeft, _cameraRight;

	private Camera _camera;

	private IInputMode _inputMode;
	private InteractiveModeInput _cameraMode;
	private EditModeInput _editMode;

	[SerializeField] private float _cameraSpeed = 50.0f;
	[SerializeField] private Canvas _EditCanvas = null;
	[SerializeField] private Grid _grid;

	public GameObject groundObject;

	void Start()
	{
		_camera = Camera.main;
		_camera.transform.forward = new Vector3(-0.5f, -0.6f, 0.7f);

		_cameraMode = new InteractiveModeInput(_camera);
		_editMode = new EditModeInput(_camera, groundObject);
		_inputMode = _cameraMode;
		_EditCanvas.enabled = false;
	}

	void Update()
    {
		if (Input.GetKey(KeyCode.A))
		{
			_cameraLeft = new CameraMoveLeft(_camera.transform, _cameraSpeed);
			_cameraLeft.Excute();
		}
		else if (Input.GetKey(KeyCode.D))
		{
			_cameraRight = new CameraMoveRight(_camera.transform, _cameraSpeed);
			_cameraRight.Excute();
		}
		else if (Input.GetKey(KeyCode.W))
		{
			_cameraUp = new CameraMoveUp(_camera.transform, _cameraSpeed);
			_cameraUp.Excute();
		}
		else if (Input.GetKey(KeyCode.S))
		{
			_cameraDown = new CameraMoveDown(_camera.transform, _cameraSpeed);
			_cameraDown.Excute();
		}
		else if(Input.GetKeyDown(KeyCode.R))
		{
			//YZ평면을 기준으로 방향벡터 대칭이동
			Vector3 newForward = _camera.transform.forward;
			newForward.x *= -1;
			_camera.transform.forward = newForward;

			Plane plane = new Plane(Vector3.up, 0);

			float distance;
			Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
			if (plane.Raycast(ray, out distance))
			{
				Vector3 newPos = ray.GetPoint(distance);
				_camera.transform.position -= newPos;
			}
		}

		_inputMode.Update();
	}

	public void OnCameraMode()
	{		
		_EditCanvas.enabled = false;
		_inputMode = _cameraMode;
		GameManager.Instance.playerInputMode = _inputMode;

		_editMode.SwitchPreview(false);
	}

	public void OnEditMode()
	{
		_EditCanvas.enabled = true;
		_inputMode = _editMode;
		GameManager.Instance.playerInputMode = _inputMode;

		_editMode.SwitchPreview(true);
	}
}
