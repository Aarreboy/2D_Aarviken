using UnityEngine;
using UnityEngine.UI;  // To access UI elements

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 100;  // Max mana the player can have
    public int currentMana;     // Current mana the player has
    public float manaRegenRate = 20f;  // How fast mana regenerates per second (try lowering this to make it noticeable)

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
        // Regenerate mana over time
        //RegenerateMana();

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

    // Regenerate mana over time
    //void RegenerateMana()
    //{
    //    if (currentMana < maxMana)
    //    {
    //        // Increment mana by regeneration rate over time
    //        currentMana += Mathf.FloorToInt(manaRegenRate * Time.deltaTime);
    //        if (currentMana > maxMana) currentMana = maxMana; // Ensure it doesn't go over the max
    //    }
    //}
}
