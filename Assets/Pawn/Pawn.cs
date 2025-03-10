using System.Collections.Generic;
using UnityEngine;

public class Pawn : Attackable
{
    public Brain m_brain;
    public Transform m_body;
    public Rigidbody m_physics;
    public float m_speed;
    public float m_balance;

    PawnState currentState;

    Dictionary<PawnStateType, PawnState> m_lookUpState = new Dictionary<PawnStateType, PawnState>();

    public float stunTime = 0;

    protected override void Awake()
    {
        base.Awake();
        currentState = new IdlePawnState();
        currentState.Initialize(this, m_lookUpState);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        m_brain.OnDamaged(damage);
    }

    public override void Hit(Hazard damage)
    {
        base.Hit(damage);
        m_physics.AddForce(damage.pushForce, ForceMode.Impulse);
        if (damage.stun > stunTime) stunTime = damage.stun;
    }

    protected virtual void Update()
    {
        m_brain.ZeroCommands();

        while(true)
        {
            PawnStateType nextState = currentState.Update();
            if (nextState == currentState.stateType) break;
            
            currentState.Exit();
            currentState = m_lookUpState[nextState];
            currentState.Enter();
        }
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

}


public enum PawnStateType
{
    Idle,
    Prepare,
    Attack,
    Defend,
    Run,
    Interact,
}

public abstract class PawnState
{
    public PawnStateType stateType;
    Pawn m_user;

    protected PawnState(){ }

    public virtual void Initialize(Pawn user, Dictionary<PawnStateType, PawnState> lookUpState)
    {
        m_user = user;
        lookUpState.Add(stateType, this);
    }

    public virtual void Enter()
    {

    }

    public virtual PawnStateType Update()
    {
        if (m_user.stunTime <= 0)
        {
            m_user.m_brain.UpdateCommands();
            m_user.m_body.localRotation *= Quaternion.AngleAxis(m_user.m_brain.commands.spin * Time.deltaTime, Vector3.up);
        }
        else m_user.stunTime -= Time.deltaTime;
        return this.stateType;
    }

    public virtual void FixedUpdate()
    {
        m_user.m_physics.AddForce((m_user.m_body.forward * m_user.m_brain.commands.forwards + m_user.m_body.right * m_user.m_brain.commands.rightwards).normalized * m_user.m_speed);
    }

    public virtual void Exit()
    {

    }
}
