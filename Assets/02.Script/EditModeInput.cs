using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeInput : IInputMode
{
	private Camera _camera;
	private GameObject _groundRoot;
	private GameObject _previewObject;
	private GameObject _selectedPrefab;

	private bool _bCanBuild;

	public EditModeInput(Camera camera, GameObject groundObject, GameObject selectedPrefab)
	{
		_camera = camera;
		_groundRoot = groundObject;
		_selectedPrefab = selectedPrefab;

		_previewObject = Object.Instantiate(_selectedPrefab);
		_previewObject.transform.localScale = Vector3.one;
		_previewObject.transform.position = Vector3.zero;
		_previewObject.name = "PreviewObject";
		_previewObject.layer = LayerMask.NameToLayer("Default");
		_previewObject.SetActive(false);
	}

	public void Update()
	{
		RaycastHit hit;
		LayerMask mask = LayerMask.GetMask("Tile");
		Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
		{
			if (hit.collider)
			{
				Vector3 newPos = hit.collider.transform.position;
				newPos.y = 0;
				_previewObject.transform.position = newPos;

				if (hit.collider.tag == "Unexplored")
				{
					_bCanBuild = false;
					SetPreviewObjectColor(new Color(0.8f, 0, 0, 1.0f));
				}
				else if(_selectedPrefab.tag == "Ground")
				{
					//땅으로 만드는 타일. 모든 타일위에 설치가능
					_bCanBuild = true;
					SetPreviewObjectColor(new Color(0.6f, 1f, 0.55f, 1.0f));
				}
				else
				{
					_bCanBuild = true;
					SetPreviewObjectColor(new Color(0.6f, 1f, 0.55f, 1.0f));
				}
			}
		}

		if (_bCanBuild)
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (hit.collider)
				{
					Vector3 newPos = hit.collider.transform.position;
					newPos.y = 0;
					GameObject go = Object.Instantiate(_selectedPrefab);
					go.transform.position = newPos;
					go.transform.localScale = Vector3.one;
					go.transform.rotation = Quaternion.identity;
					go.transform.SetParent(_groundRoot.transform);
					Object.Destroy(hit.collider.gameObject);
				}
			}
		}
	}

	public void SwitchPreview(bool val)
	{
		_previewObject.SetActive(val);
	}

	public void SetPreviewObjectColor(Color color)
	{
		var comps = _previewObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < comps.Length; i++)
		{
			comps[i].material.color = color;
		}
	}
}
