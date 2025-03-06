using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Drag the projectile prefab in Unity Inspector
    public Transform firePoint; // The position where the projectile will spawn
    public int manaCost = 10;  // Mana cost for each attack
    public float fireForce = 10f;

    private PlayerMana playerMana;

    void Start()
    {
        playerMana = GetComponent<PlayerMana>();  // Assuming PlayerMana script is attached to the player
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (playerMana.UseMana(manaCost))  // Check if the player has enough mana to attack
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<MagicProjectile>().Initialize(firePoint.forward);
        }
    }
}
