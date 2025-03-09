using UnityEngine;

public class Attackable : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100;
    protected float currentHealth;  // "protected" s� att �rvande klasser kan komma �t det

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //Debug.Log(gameObject.name + " took " + damage + " damage. Remaining health: " + currentHealth);

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public virtual void Hit(float damage)
    {
        TakeDamage(damage);
        Debug.Log(gameObject.name + " was hit!");
        // L�gg till effekter h�r (ex. blinkande sprite, ljud)
    }

    public virtual void OnDeath()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
