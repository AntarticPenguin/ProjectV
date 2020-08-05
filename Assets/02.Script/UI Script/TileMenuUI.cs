using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMenuUI : MonoBehaviour
{
	public GameObject _scrollContainer;

    void Start()
	{
		var sprites = ResourceManager.Instance.GetSpriteMap();
		foreach (var item in sprites)
		{
			GameObject go = new GameObject();
			var img = go.AddComponent<Image>();
			img.sprite = item.Value;
			go.transform.SetParent(_scrollContainer.transform);

			var contentItem = go.AddComponent<ContentItem>();
			TileObject tileObject = new TileObject(ResourceManager.Instance.GetTilePrefab(item.Key), 500);
			contentItem.tileObject = tileObject;
		}
	}
}
