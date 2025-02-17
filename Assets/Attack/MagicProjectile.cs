using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public GameObject explosionPrefab;  // Reference to the explosion effect prefab

    private Vector3 direction;

    public void Initialize(Vector3 shootDirection)
    {
        direction = shootDirection.normalized;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Instantiate the explosion effect at the projectile's position
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
