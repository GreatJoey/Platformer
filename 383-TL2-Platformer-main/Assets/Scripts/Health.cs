using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public bool destroyOnDeath = true;
    public int maxHealth = 3;
    public int currentHealth;
    public HudScript healthBarMove;

    // added by Connor
    [SerializeField] SMScript sound_manager;
    private void Awake()
    {
        GameObject sm_obj = GameObject.Find("SoundManager");
        if (sm_obj)
            sound_manager = sm_obj.GetComponent<SMScript>();
    }
    // --------------

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) 
        { 
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{name} took {amount} dmg. HP: {currentHealth}/{maxHealth}");
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Die();
            }
            Debug.Log($"{name} died");
            // TODO: respawn / game over / disable controls
        }
    }
    
    private void Die()
    {
        // added Connor
        sound_manager.DefeatSound();
        // ------------
        if (destroyOnDeath) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
}
