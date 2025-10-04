using UnityEngine;

public class CoinSound : ICoinBehavior
{
    private SMScript soundManager;

    public CoinSound(SMScript sm)
    {
        soundManager = sm;
    }

    //  ?   <---Null Conditional Op == "no errors pls"
    public void Execute(Coin coin, Collider2D other)
    {
        soundManager?.CollectableSound();
    }
}
