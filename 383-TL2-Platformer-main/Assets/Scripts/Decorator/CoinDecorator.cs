using UnityEngine;

public class CoinDecorator : ICoinBehavior
{
    private ICoinBehavior[] behaviors;

    public CoinDecorator(params ICoinBehavior[] behaviors)
    {
        this.behaviors = behaviors;
    }

    public void Execute(Coin coin, Collider2D other)
    {
        foreach (var behavior in behaviors)
        {
            behavior.Execute(coin, other);
        }
    }
}
