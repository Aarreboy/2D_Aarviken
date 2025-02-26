using UnityEngine;
using UnityEngine.UI;  // Import the UI namespace to interact with the Slider

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    // Reference to the UI Slider (Health Bar)
    public Slider healthBar;

    void Start()
    {
        currentHealth = 50;

        // Make sure the health bar is set up correctly at the start
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth; // Set the max value of the health bar
            healthBar.value = currentHealth; // Set the initial value of the health bar
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // Update the health bar whenever health changes
        UpdateHealthBar();

        Debug.Log("Player Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Prevent health from going negative
            Debug.Log("Player is dead!");
        }

        // Update the health bar whenever health changes
        UpdateHealthBar();
    }

    // Method to update the health bar
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth; // Set the value of the health bar
        }
    }
}
