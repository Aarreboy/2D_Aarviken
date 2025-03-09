using Unity.VisualScripting;
using UnityEngine;

public class MagicProjectile : Attackable
{
    public float speed = 10f;
    public float lifetime = 3f;
    public float damage = 25;
    [SerializeField] ExplosionManager explosionManager;  // Reference to the explosion effect prefab
    [SerializeField] Rigidbody physics;

    public void Initialize(Vector3 shootDirection)
    {
        transform.rotation = Quaternion.LookRotation(shootDirection, Vector3.up);
        explosionManager.TriggerParticles1(transform.position);
        physics.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosionManager.TriggerParticles1(transform.position);

        collision.collider?.GetComponent<Attackable>()?.Hit(damage);

    }

    void OnTriggerEnter(Collider other)
    {
        // Instantiate the explosion effect at the projectile's position
        explosionManager.TriggerParticles1(transform.position);
        
        other?.GetComponent<Attackable>()?.Hit(damage);
        OnDeath();
    }
}
