using UnityEngine;

public class CoinCollect : ICoinBehavior
{
    public void Execute(Coin coin, Collider2D other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.AddCoin(coin.coinData.value);
            Object.Destroy(coin.gameObject);
        }
    }
}
