using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PawnAttributes
{
    public Transform m_body;
    public Rigidbody m_physics;
    public float m_speed;
    public float m_balance;
    public float stunTime = 0;
    public Tool[] tools;
    public int selectedToolIndex;
}

public class Pawn : Attackable
{
    public Brain m_brain;
    [SerializeField] PawnAttributes m_attributes;
    PawnState currentState;
    Dictionary<PawnStateType, PawnState> m_lookUpState = new Dictionary<PawnStateType, PawnState>();
    protected override void Awake()
    {
        base.Awake();
        m_brain.Initialize(m_attributes);
        currentState = new IdlePawnState();
        currentState.Initialize(m_brain, m_attributes, m_lookUpState);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        m_brain.OnDamaged(damage);
    }

    public override void Hit(Hazard damage)
    {
        base.Hit(damage);
        m_attributes.m_physics.AddForce(damage.pushForce, ForceMode.Impulse);
        if (damage.stun > m_attributes.stunTime) m_attributes.stunTime = damage.stun;
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
    Brain m_brain;
    PawnAttributes m_attributes;

    protected PawnState(){ }

    public virtual void Initialize(Brain brain, PawnAttributes attributes, Dictionary<PawnStateType, PawnState> lookUpState)
    {
        m_brain = brain;
        m_attributes = attributes;
        lookUpState.Add(stateType, this);
    }

    public virtual void Enter()
    {

    }

    public virtual PawnStateType Update()
    {
        if (m_attributes.stunTime <= 0)
        {
            m_brain.UpdateCommands();
            m_attributes.m_body.localRotation *= Quaternion.AngleAxis(m_brain.commands.spin * Time.deltaTime, Vector3.up);
        }
        else m_attributes.stunTime -= Time.deltaTime;
        return this.stateType;
    }

    public virtual void FixedUpdate()
    {
        m_attributes.m_physics.AddForce((m_attributes.m_body.forward * m_brain.commands.forwards + m_attributes.m_body.right * m_brain.commands.rightwards).normalized * m_attributes.m_speed);
    }

    public virtual void Exit()
    {

    }
}
