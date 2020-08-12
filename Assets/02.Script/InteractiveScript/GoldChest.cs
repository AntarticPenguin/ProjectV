using UnityEngine;

public class GoldChest : MonoBehaviour, IInteractive
{
	public int GoldAmount;

	public void Interact()
	{
		if(GoldAmount > 0)
		{
			CurrencySystem.Instance.AddGold(GoldAmount);
			Destroy(gameObject);
		}
	}
}
