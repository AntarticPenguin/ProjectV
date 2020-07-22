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
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					if(hit.collider.tag == "GoldChest")
					{
						//보물상자 클릭. 골드획득
						var comp = hit.collider.GetComponent<IInteractive>();
						comp.Interact();
					}
					else
					{
						//카메라를 타겟 위치로 이동시킨다
						Vector3 target = hit.collider.transform.position;
						if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit))
						{
							if (hit.collider)
							{
								Vector3 orig = hit.collider.transform.position;
								Vector3 newPos = target - orig;
								newPos.y = 0;
								_camera.transform.position += newPos;
							}
						}
					}
				}
			}
		}
	}
}
