using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveRight : ICommand
{
	private float _speed;

	public CameraMoveRight(float speed)
	{
		_speed = speed;
	}

	public void Excute()
	{
		var camTransform = GameManager.Instance.GetMainCamera().transform;
		camTransform.Translate(camTransform.right * _speed * Time.deltaTime, Space.World);
		//_camera.Translate(_camera.right * _speed * Time.deltaTime, Space.World);
	}
}
