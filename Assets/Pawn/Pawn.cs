using UnityEngine;

public class Pawn : Attackable
{
    public Brain m_brain;
    public Transform m_body;
    public Rigidbody m_physics;
    public float m_speed;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        m_brain.OnDamaged(damage);
    }

}