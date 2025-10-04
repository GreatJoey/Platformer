using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI GoldCoin;
    public TextMeshProUGUI SilverCoin;
    private int goldMemory = 0;
    private int silverMemory = 0;

    void Start()
    {
        GoldCoin.text = goldMemory.ToString();
        SilverCoin.text = silverMemory.ToString();
    }

    void Update()
    {

    }

    public void IncreaseCoin(int amount, string type)
    {
        
        if (type == "gold")
        {
            goldMemory += amount;
            GoldCoin.text = goldMemory.ToString();

        }

        if (type == "silver")
        {
            silverMemory += amount;
            SilverCoin.text = silverMemory.ToString();
        }
    }
}