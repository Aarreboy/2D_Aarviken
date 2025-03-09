using UnityEngine;

public class Pawn : Attackable
{
    public Brain m_brain;
    public Transform m_body;
    public Rigidbody m_physics;
    public float m_speed;

    float stunTime = 0;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        m_brain.OnDamaged(damage);
    }

    public override void Hit(Hazard damage)
    {
        base.Hit(damage);
        m_physics.AddForce(damage.pushForce, ForceMode.Impulse);
        if(damage.stun > stunTime) stunTime = damage.stun;
    }

    protected virtual void Update()
    {
        m_brain.ZeroCommands();

        if (stunTime <= 0)
        {
            m_brain.UpdateCommands();
            m_body.localRotation *= Quaternion.AngleAxis(m_brain.commands.spin * Time.deltaTime, Vector3.up);
        }
        else stunTime -= Time.deltaTime;

    }

    protected virtual void FixedUpdate()
    {
        m_physics.AddForce((m_body.forward * m_brain.commands.forwards + m_body.right * m_brain.commands.rightwards).normalized * m_speed);
    }

}