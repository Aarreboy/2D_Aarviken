using UnityEngine;
using UnityEngine.UI;  // To access UI elements

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 100;  // Max mana the player can have
    public int currentMana;     // Current mana the player has
    public float manaRegenRate = 50f;  // How fast mana regenerates per second (try lowering this to make it noticeable)

    public Slider manaBar;  // Reference to the UI Slider that shows the mana bar

    void Start()
    {
        // Set the initial mana value
        currentMana = maxMana;

        // Set the initial value of the mana slider
        if (manaBar != null)
        {
            manaBar.maxValue = maxMana;
            manaBar.value = currentMana;
        }
    }

    void Update()
    {
        
        // Update the mana bar UI
        if (manaBar != null)
        {
            manaBar.value = currentMana;
        }

        Debug.Log("Current Mana: " + currentMana);
    }

    void FixedUpdate()
    {
        RegenerateMana();
    }

    // Method to use mana for an attack
    public bool UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;  // Mana used successfully
        }
        else
        {
            return false;  // Not enough mana
        }
    }

    // Regenerate mana over time
    private float manaRegenBuffer = 0f;  // Stores decimal regeneration values

    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            // Accumulate mana regen in a float buffer
            manaRegenBuffer += manaRegenRate * Time.deltaTime;

            // Convert buffer into whole mana points
            int manaToAdd = Mathf.FloorToInt(manaRegenBuffer);
            if (manaToAdd > 0)
            {
                currentMana += manaToAdd;
                manaRegenBuffer -= manaToAdd; // Remove the used amount from buffer
            }

            // Ensure mana does not exceed maxMana
            if (currentMana > maxMana) currentMana = maxMana;
        }
    }


}
