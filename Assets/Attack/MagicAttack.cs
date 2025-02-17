using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Drag the projectile prefab in Unity Inspector
    public Transform firePoint; // The position where the projectile will spawn
    public float fireForce = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Shoot();
        }
    }

    void Shoot()
    {

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Use firePoint's forward direction
        projectile.GetComponent<MagicProjectile>().Initialize(firePoint.transform.forward);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
