using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinData coinData;
    private ICoinBehavior coinBehavior;
    public CoinHud Collector;

    // added by Connor
    [SerializeField] SMScript sound_manager;
    private void Awake()
    {
        GameObject sm_obj = GameObject.Find("SoundManager");
        if (sm_obj)
            sound_manager = sm_obj.GetComponent<SMScript>();

        // Decorator Pattern -- Mikayla
        coinBehavior = new CoinDecorator(
            new CoinSound(sound_manager),
            new CoinCollect()
        );
        // -----------------
    }
    // -----------------
    // Decorator Pattern    --    Mikayla
    public void OnTriggerEnter2D(Collider2D other)
    {
        coinBehavior.Execute(this, other);
        
        if (coinData.coinName.Contains("Gold"))
        {
            Collector.IncreaseCoin("Gold");
        }

        if (coinData.coinName.Contains("Silver"))
        {
            Collector.IncreaseCoin("Silver");
        }
    }
}
