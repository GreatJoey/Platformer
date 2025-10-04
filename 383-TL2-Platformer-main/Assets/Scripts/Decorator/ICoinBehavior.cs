using UnityEngine;

public interface ICoinBehavior
{
    void Execute(Coin coin, Collider2D other);
}
