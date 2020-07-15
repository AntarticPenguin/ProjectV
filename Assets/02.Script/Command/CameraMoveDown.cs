using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveDown : ICommand
{
	private Transform _camera;
	private float _speed;

	public CameraMoveDown(Transform cameraTransform, float speed)
	{
		_camera = cameraTransform;
		_speed = speed;
	}

	public void Excute()
	{
		Vector3 horizontalDir = Quaternion.AngleAxis(-36.03f, _camera.right) * _camera.forward;
		_camera.Translate(-horizontalDir * _speed * Time.deltaTime, Space.World);
	}
}
