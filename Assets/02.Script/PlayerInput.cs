using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerInput : MonoBehaviour
{
	private ICommand _cameraUp, _cameraDown, _cameraLeft, _cameraRight;
	public Joystick _joystick;

	private Camera _camera;

	private IInputMode _inputMode;
	private InteractiveModeInput _cameraMode;
	private EditModeInput _editMode;

	[SerializeField] private float _cameraSpeed = 50.0f;
	[SerializeField] private Canvas _EditCanvas = null;

	public GameObject groundObject;

	void Start()
	{
		_camera = Camera.main;
		_camera.transform.forward = new Vector3(-0.5f, -0.6f, 0.7f);

		_cameraMode = new InteractiveModeInput();
		_editMode = new EditModeInput(_camera, groundObject);

		GameManager.Instance._playerInput = this;
		GameManager.Instance._interactiveModeInput = _cameraMode;
		GameManager.Instance._editModeInput = _editMode;
		_inputMode = _cameraMode;
		_EditCanvas.enabled = false;
	}

	void Update()
    {
		if (Input.GetKey(KeyCode.A) || _joystick.Horizontal == -1 )
		{
			_cameraLeft = new CameraMoveLeft(_cameraSpeed);
			_cameraLeft.Excute();
		}
		else if (Input.GetKey(KeyCode.D) || _joystick.Horizontal == 1)
		{
			_cameraRight = new CameraMoveRight(_cameraSpeed);
			_cameraRight.Excute();
		}
		else if (Input.GetKey(KeyCode.W) || _joystick.Vertical == 1)
		{
			_cameraUp = new CameraMoveUp(_cameraSpeed);
			_cameraUp.Excute();
		}
		else if (Input.GetKey(KeyCode.S) || _joystick.Vertical == -1)
		{
			_cameraDown = new CameraMoveDown(_cameraSpeed);
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

		_editMode.SwitchPreview(false);
	}

	public void OnEditMode()
	{
		_EditCanvas.enabled = true;
		_inputMode = _editMode;

		_editMode.SwitchPreview(true);
	}
}
