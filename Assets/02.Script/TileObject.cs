using UnityEngine;

public class TileObject
{
	public TileObject(GameObject prefab, int cost)
	{
		TilePrefab = prefab;
		Cost = cost;
	}

	public GameObject TilePrefab { get; set; }
	public int Cost { get; set; }
}
