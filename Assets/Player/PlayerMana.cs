using UnityEngine;
using UnityEngine.UI;  // To access UI elements

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 100;  // Max mana the player can have
    public float currentMana;     // Current mana the player has
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
        RegenerateMana();
        // Update the mana bar UI
        if (manaBar != null)
        {
            manaBar.value = currentMana;
        }

        Debug.Log("Current Mana: " + currentMana);
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


    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime; // Smooth regeneration
            if (currentMana > maxMana) currentMana = maxMana; // Clamp to maxMana
        }
    }



}
