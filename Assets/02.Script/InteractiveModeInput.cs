using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class InteractiveModeInput : IInputMode
{
	private ICommand _cameraTarget;

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
					if(hit.collider.tag == "InteractiveObject")
					{
						//상호작용이 가능한 오브젝트 실행
						var comp = hit.collider.GetComponent<IInteractive>();
						comp.Interact();
					}
					else
					{
						//카메라를 타겟 위치로 이동시킨다
						Vector3 target = hit.collider.transform.position;
						CinemachineVirtualCamera camera = GameManager.Instance.GetMainCamera();

						if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
						{
							if (hit.collider)
							{
								Vector3 orig = hit.collider.transform.position;
								Vector3 newPos = target - orig;
								newPos.y = 0;
								camera.transform.position += newPos;
							}
						}
					}
				}
			}
		}
	}
}
