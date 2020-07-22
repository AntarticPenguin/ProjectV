using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	private static ResourceManager _instance;

	public static ResourceManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<ResourceManager>();
				if (_instance == null)
				{
					GameObject go = new GameObject();
					_instance = go.AddComponent<ResourceManager>();
					go.name = "ResourceManager";
				}
			}
				
			return _instance;
		}
	}

	private Dictionary<string, Sprite> _spriteMap = new Dictionary<string, Sprite>();
	private Dictionary<string, GameObject> _tileMap = new Dictionary<string, GameObject>();

	private void Awake()
	{
		LoadTileSprite();
		LoadTilePrefab();

		DontDestroyOnLoad(gameObject);
	}

	private void LoadTileSprite()
	{
		Sprite[] sprites = Resources.LoadAll<Sprite>("TileSheet");
		for (int i = 0; i < sprites.Length; ++i)
		{
			if (!_spriteMap.ContainsKey(sprites[i].name))
				_spriteMap.Add(sprites[i].name, sprites[i]);
		}
	}

	private void LoadTilePrefab()
	{
		GameObject[] tilePrefabs = Resources.LoadAll<GameObject>("Tiles/");
		for(int i = 0; i < tilePrefabs.Length; ++i)
		{
			if (!_tileMap.ContainsKey(tilePrefabs[i].name))
				_tileMap.Add(tilePrefabs[i].name, tilePrefabs[i]);
		}
	}

	public Dictionary<string, Sprite> GetSpriteMap() { return _spriteMap; }

	public GameObject GetTilePrefab(string name)
	{
		if (_tileMap.ContainsKey(name))
			return _tileMap[name];
		return null;
	}
}
