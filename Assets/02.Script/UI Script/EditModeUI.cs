using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditModeUI : MonoBehaviour
{
	public GameObject _scrollContainer;

	public Button _upButton;
	public Button _downButton;
	public Button _leftButton;
	public Button _rightButton;

	public Button _rotateButton;		//clockwise
	public Button _rotateReverseButton;

	public Button _heightUpButton;
	public Button _heightDownButton;

	private int _cellSize;
	private int _rotateAngle;
	private int _heightSize;

	private void Start()
	{
		_upButton.onClick.AddListener(delegate { MoveTileVertical(1); });
		_downButton.onClick.AddListener(delegate { MoveTileVertical(-1); });
		_rightButton.onClick.AddListener(delegate { MoveTileHorizontal(1); });
		_leftButton.onClick.AddListener(delegate { MoveTileHorizontal(-1); });

		_rotateButton.onClick.AddListener(delegate { RotateTile(); });
		_rotateReverseButton.onClick.AddListener(delegate { RotateTile(-1); });

		_cellSize = 2;
		_rotateAngle = 90;
		_heightSize = 1;

		var sprites = ResourceManager.Instance.GetSpriteMap();
		foreach (var item in sprites)
		{
			GameObject go = new GameObject();
			var img = go.AddComponent<Image>();
			img.sprite = item.Value;
			go.transform.SetParent(_scrollContainer.transform);

			var contentItem = go.AddComponent<ContentItem>();
			contentItem.prefab = ResourceManager.Instance.GetTilePrefab(item.Key);
		}
	}

	private void MoveTileVertical(int direction)
	{
		var inputMode = GameManager.Instance.playerInputMode as EditModeInput;
		GameObject preview = inputMode.GetPreviewPrefab();

		if(preview)
		{
			Vector3 newPos = preview.transform.position;
			newPos.z += direction * _cellSize;
			preview.transform.position = newPos;
		}
	}

	private void MoveTileHorizontal(int direction)
	{
		var inputMode = GameManager.Instance.playerInputMode as EditModeInput;
		GameObject preview = inputMode.GetPreviewPrefab();

		if (preview)
		{
			Vector3 newPos = preview.transform.position;
			newPos.x += direction * _cellSize;
			preview.transform.position = newPos;
		}
	}

	private void RotateTile(int clockwise = 1)
	{
		var inputMode = GameManager.Instance.playerInputMode as EditModeInput;
		GameObject preview = inputMode.GetPreviewPrefab();

		if(preview)
		{
			Vector3 angles = preview.transform.rotation.eulerAngles;
			angles.y += clockwise * _rotateAngle;
			Quaternion newRot = Quaternion.Euler(angles);
			preview.transform.rotation = newRot;
		}
	}


	private void AdjustHeightTile(int direction)
	{
		var inputMode = GameManager.Instance.playerInputMode as EditModeInput;
		GameObject preview = inputMode.GetPreviewPrefab();

		if(preview)
		{
			Vector3 newPos = preview.transform.position;
			newPos.y += direction * _heightSize;
			preview.transform.position = newPos;
		}
	}
}
