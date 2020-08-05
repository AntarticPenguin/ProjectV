using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeContext : Context
{
	private void Awake()
	{
		UIManager.Instance._contextDic.Add("EditMode", this);
	}

	public void MoveTileVertical(string direction)
	{
		var inputMode = GameManager.Instance._editModeInput;
		GameObject preview = inputMode.GetPreviewPrefab();

		if (preview)
		{
			Vector3 newPos = preview.transform.position;
			newPos.z += int.Parse(direction) * inputMode.CellSize;
			if(inputMode.MinXZ < newPos.z && newPos.z < inputMode.MaxXZ)
			{
				preview.transform.position = newPos;
				inputMode.CheckTile();
			}
		}
	}

	public void MoveTileHorizontal(string direction)
	{
		var inputMode = GameManager.Instance._editModeInput;
		GameObject preview = inputMode.GetPreviewPrefab();

		if (preview)
		{
			Vector3 newPos = preview.transform.position;
			newPos.x += int.Parse(direction) * inputMode.CellSize;
			if (inputMode.MinXZ < newPos.x && newPos.x < inputMode.MaxXZ)
			{
				preview.transform.position = newPos;
				inputMode.CheckTile();
			}
		}
	}

	public void RotateTile(string clockwise)
	{
		var inputMode = GameManager.Instance._editModeInput;
		GameObject preview = inputMode.GetPreviewPrefab();

		if (preview)
		{
			Vector3 angles = preview.transform.rotation.eulerAngles;
			angles.y += int.Parse(clockwise) * inputMode.RotateAngle;
			Quaternion newRot = Quaternion.Euler(angles);
			preview.transform.rotation = newRot;

			inputMode.CheckTile();
		}
	}

	public void AdjustHeightTile(string direction)
	{
		var inputMode = GameManager.Instance._editModeInput;
		GameObject preview = inputMode.GetPreviewPrefab();

		if (preview)
		{
			Vector3 newPos = preview.transform.position;
			newPos.y += int.Parse(direction) * inputMode.HeightSize;
			if(inputMode.MinHeight < newPos.y && newPos.y < inputMode.MaxHeight)
			{
				preview.transform.position = newPos;
				inputMode.CheckTile();
			}
		}
	}

	public void BuildTile()
	{
		var inputMode = GameManager.Instance._editModeInput;
		inputMode.BuildTile();
	}

	public void Cancel()
	{
		var inputMode = GameManager.Instance._editModeInput;
		inputMode.Cancel();
	}
}
