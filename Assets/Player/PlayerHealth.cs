using UnityEngine;
using UnityEngine.UI;  // Import the UI namespace to interact with the Slider

public class PlayerHealth : MonoBehaviour
{
    
    // Reference to the UI Slider (Health Bar)
    public Slider healthBar;
    Attackable target;
    void Start()
    {
        target = GetComponent<Attackable>();
        // Make sure the health bar is set up correctly at the start
        if (healthBar != null)
        {
            healthBar.maxValue = target.GetMaxHealth(); // Set the max value of the health bar
            healthBar.value = target.GetHealth(); // Set the initial value of the health bar
        }
    }

       
    // Method to update the health bar
    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = target.GetHealth(); // Set the value of the health bar
        }
    }
}
