using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveDown : ICommand
{
	private float _speed;

	public CameraMoveDown(float speed)
	{
		_speed = speed;
	}

	public void Excute()
	{
		var camTransform = GameManager.Instance.GetMainCamera().transform;
		float angle = camTransform.eulerAngles.x;

		Vector3 horizontalDir = Quaternion.AngleAxis(-angle, camTransform.right) * camTransform.forward;
		camTransform.Translate(-horizontalDir * _speed * Time.deltaTime, Space.World);
	}
}
