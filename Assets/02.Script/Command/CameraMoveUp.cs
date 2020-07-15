using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveUp : ICommand
{
	private Transform _camera;
	private float _speed;

	public CameraMoveUp(Transform cameraTransform, float speed)
	{
		_camera = cameraTransform;
		_speed = speed;
	}

	public void Excute()
	{
		Vector3 horizontalDir = Quaternion.AngleAxis(-36.03f, _camera.right) * _camera.forward;
		//Vector3 newPos = _camera.transform.position;
		//newPos += horizontalDir * _speed * Time.deltaTime;
		//_camera.position = newPos;
		_camera.Translate(horizontalDir * _speed * Time.deltaTime, Space.World);
	}
}
