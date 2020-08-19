using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditModeInput : IInputMode
{
	private Camera _camera;
	private GameObject _groundRoot;
	private GameObject _previewObject;
	private TileObject _selectedTileObject;

	private bool _bCanBuild;
	private GameObject _curHitObject;

	public int MinHeight { get; set; }
	public int MaxHeight { get; set; }
	public int MinXZ { get; set; }
	public int MaxXZ { get; set; }

	public int CellSize { get; set; }
	public int RotateAngle { get; set; }
	public int HeightSize { get; set; }

	public EditModeInput(Camera camera, GameObject groundObject)
	{
		_camera = camera;
		_groundRoot = groundObject;

		MinHeight = -3;
		MaxHeight = 20;
		MinXZ = -20;
		MaxXZ = 20;

		CellSize = 2;
		RotateAngle = 90;
		HeightSize = 1;
	}

	public void Update()
	{
		if (_previewObject == null)
			return;

		if(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
		{
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				if(hit.collider)
				{
					Vector3 newPos = hit.collider.transform.position;
					newPos.y = _previewObject.transform.position.y;
					_previewObject.transform.position = newPos;
					CheckTile();
				}
			}
		}
	}

	public void SwitchPreview(bool val)
	{
		if(_previewObject != null)
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

	public void SetSelectedTileObject(TileObject tileObject)
	{
		if (_previewObject != null)
			Object.DestroyImmediate(_previewObject);

		_selectedTileObject = tileObject;

		_previewObject = Object.Instantiate(_selectedTileObject.TilePrefab);
		_previewObject.transform.localScale = Vector3.one;
		_previewObject.transform.position = Vector3.zero;
		_previewObject.name = "PreviewObject";
		_previewObject.layer = LayerMask.NameToLayer("Default");
	}

	public GameObject GetPreviewPrefab()
	{
		return _previewObject;
	}

	public void CheckTile()
	{
		//수직으로 광선을 쏴 타일 태그 확인
		RaycastHit hit;
		LayerMask mask = LayerMask.GetMask("Tile");
		Vector3 to = _previewObject.transform.position;
		Vector3 from = _previewObject.transform.position;
		from.y += 40;
		
		Ray ray = new Ray(from, to - from);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
		{
			if (hit.collider)
			{
				if (hit.collider.tag == "Unexplored")
				{
					_bCanBuild = false;
					SetPreviewObjectColor(new Color(0.8f, 0, 0, 1.0f));
				}
				else if (_selectedTileObject.TilePrefab.tag == "Ground")
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

			if(_bCanBuild)
			{
				_curHitObject = hit.collider.gameObject;
			}
		}
	}

	public void BuildTile()
	{
		if (_bCanBuild)
		{
			int cost = _selectedTileObject.Cost;

			if(CurrencySystem.Instance.UseGold(cost))
			{
				GameObject go = Object.Instantiate(_selectedTileObject.TilePrefab);
				go.transform.position = _previewObject.transform.position;
				go.transform.localScale = Vector3.one;
				go.transform.rotation = _previewObject.transform.rotation;
				go.transform.SetParent(_groundRoot.transform);
				Object.Destroy(_curHitObject);

				if(GameManager.Instance._isPlayingTutorial)
				{
					TutorialPlay.Instance._bBuildTileSuccess = true;
				}
			}
			else
			{
				Debug.Log("골드가 부족합니다");
			}
		}
	}

	public void Cancel()
	{
		if(_previewObject != null)
			Object.DestroyImmediate(_previewObject);
	}
}
