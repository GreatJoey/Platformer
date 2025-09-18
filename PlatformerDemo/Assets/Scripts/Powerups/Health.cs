using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 3;
    public int CurrentHealth = 3;

    public void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
