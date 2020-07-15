using UnityEngine;

public class CameraMoveToTarget : ICommand
{
	private Transform _camera;
	private Vector3 _input;

	public CameraMoveToTarget(Transform camera, Vector3 input)
	{
		_camera = camera;
		_input = input;
	}

	public void Excute()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(_input);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider)
			{
				//카메라를 타겟 위치로 이동시킨다
				Vector3 target = hit.collider.transform.position;
				if (Physics.Raycast(_camera.position, Camera.main.transform.forward, out hit))
				{
					if (hit.collider)
					{
						Vector3 orig = hit.collider.transform.position;
						Vector3 newPos = target - orig;
						newPos.y = 0;
						_camera.position += newPos;
					}
				}

			}
		}
	}
}
