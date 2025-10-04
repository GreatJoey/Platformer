using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public int Coins { get; private set; } = 0;

    public void AddCoin(int amount = 1)
    {
        Coins += amount;
        Debug.Log($"Player has {Coins} coins now");
    }
}
