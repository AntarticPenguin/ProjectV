using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveModeInput : IInputMode
{
	private ICommand _cameraTarget;
	private Camera _camera;

	public InteractiveModeInput(Camera camera)
	{
		_camera = camera;
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				_cameraTarget = new CameraMoveToTarget(_camera.transform, Input.mousePosition);
				_cameraTarget.Excute();
			}
		}
	}
}
