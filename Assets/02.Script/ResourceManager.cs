using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
	private Dictionary<string, TutorialScript> _scriptsMap = new Dictionary<string, TutorialScript>();
	private Dictionary<string, GameObject> _uiPrefabs = new Dictionary<string, GameObject>();

	private void Awake()
	{
		LoadTileSprite();
		LoadTilePrefab();
		LoadScript();
		LoadUIPrefab();

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

	private void LoadScript()
	{
		TextAsset textAsset = Resources.Load<TextAsset>("TutorialScript");
		var jsonData = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(textAsset.text);
		foreach(var data in jsonData)
		{
			string id = data.Key;
			TutorialScript value = data.Value.ToObject<TutorialScript>();
			_scriptsMap.Add(id, value);
		}
	}

	private void LoadUIPrefab()
	{
		GameObject[] prefabs = Resources.LoadAll<GameObject>("UI/");
		for(int i = 0; i < prefabs.Length; ++i)
		{
			if(!_uiPrefabs.ContainsKey(prefabs[i].name))
				_uiPrefabs.Add(prefabs[i].name, prefabs[i]);
		}
	}

	public Dictionary<string, Sprite> GetSpriteMap() { return _spriteMap; }
	public Dictionary<string, TutorialScript> GetScriptMap() { return _scriptsMap; }

	public GameObject GetTilePrefab(string name)
	{
		if (_tileMap.ContainsKey(name))
			return _tileMap[name];
		return null;
	}

	public GameObject GetUIPrefab(string name)
	{
		if (_uiPrefabs.ContainsKey(name))
			return _uiPrefabs[name];
		return null;
	}
}

public class TutorialScript
{
	[JsonProperty("text")]
	public string text;

	[JsonProperty("event")]
	public string eventName;
}