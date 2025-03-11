using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class PawnProperties
{
    public Transform m_body;
    public Rigidbody m_physics;
    public float m_speed;
    public float m_balance;
    public float stunTime = 0;
    public Tool[] tools;
    public int selectedToolIndex;
    public Transform actionPoint;
    public PlayerMana mana;
    public Tool selectedTool
    {
        get { return tools[selectedToolIndex]; }
        private set { }
    }
}

public class Pawn : Attackable
{
    public Brain m_brain;
    [SerializeField] PawnProperties m_properties;

    PawnState currentState;
    Dictionary<PawnStateType, PawnState> m_lookUpState = new Dictionary<PawnStateType, PawnState>();

    protected override void Awake()
    {
        base.Awake();

        m_brain.Initialize(m_properties);
        currentState = new IdlePawnState();
        currentState.Initialize(m_brain, m_properties, m_lookUpState);
        new SprintPawnState().Initialize(m_brain, m_properties, m_lookUpState);
        currentState.Enter();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        m_brain.OnDamaged(damage);
    }

    public override void Hit(Hazard damage)
    {
        base.Hit(damage);
        m_properties.m_physics.AddForce(damage.pushForce, ForceMode.Impulse);
        if (damage.stun > m_properties.stunTime) m_properties.stunTime = damage.stun;
    }

    protected virtual void Update()
    {
        m_brain.UpdateCommands();

        while (true)
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
    Sprint,
    Interact,
}

public abstract class PawnState
{
    public PawnStateType stateType;
    protected Brain m_brain;
    protected PawnProperties m_properties;

    protected PawnState() { }

    public virtual void Initialize(Brain brain, PawnProperties attributes, Dictionary<PawnStateType, PawnState> lookUpState)
    {
        m_brain = brain;
        m_properties = attributes;
        lookUpState.Add(stateType, this);
    }

    public virtual void Enter()
    {

    }

    public virtual PawnStateType Update()
    {
        m_properties.selectedToolIndex = m_brain.commands.selected;
        if (m_brain.commands.primary)
        {
            if (m_properties.mana.currentMana > m_properties.selectedTool.GetManaCost())
            {
                m_properties.mana.currentMana -= m_properties.selectedTool.GetManaCost();
                m_properties.selectedTool.StartAction(m_properties.actionPoint);
            }
        }
        m_properties.m_body.localRotation *= Quaternion.AngleAxis(m_brain.commands.spin * Time.deltaTime, Vector3.up);
        return this.stateType;
    }

    public virtual void FixedUpdate()
    {
        m_properties.m_physics.AddForce((m_properties.m_body.forward * m_brain.commands.forwards + m_properties.m_body.right * m_brain.commands.rightwards).normalized * m_properties.m_speed);
    }

    public virtual void Exit()
    {

    }
}
