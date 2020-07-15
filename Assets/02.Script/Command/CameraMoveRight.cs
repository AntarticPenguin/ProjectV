﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveRight : ICommand
{
	private Transform _camera;
	private float _speed;

	public CameraMoveRight(Transform cameraTransform, float speed)
	{
		_camera = cameraTransform;
		_speed = speed;
	}

	public void Excute()
	{
		_camera.Translate(_camera.right * _speed * Time.deltaTime, Space.World);
	}
}