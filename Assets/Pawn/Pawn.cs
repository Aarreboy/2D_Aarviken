using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains important properties of the pawn, which can be passed around as a reference together.
/// </summary>
[Serializable]
public class PawnProperties
{
    /// <summary>
    /// The part of the pawn that spins, check its forward vector to know which direction it faces.
    /// </summary>
    public Transform m_body;

    /// <summary>
    /// The pawn's rigid body.
    /// </summary>
    public Rigidbody m_physics;

    /// <summary>
    /// The pawn's movement acceleration (in regards to physics).
    /// </summary>
    public float m_speed;

    /// <summary>
    /// Should maybe be kept elsewhere. If this runs out, the pawn will be toppled.
    /// </summary>
    public float m_balance;

    /// <summary>
    /// Should maybe be kept elsewhere. A resource spent to cast spells.
    /// </summary>
    public PlayerMana mana;

    /// <summary>
    /// The readied tools for the pawn, can be 5.
    /// </summary>
    public Tool[] tools;
    public int selectedToolIndex;
    public Tool selectedTool
    {
        get { return tools[selectedToolIndex]; }
        private set { }
    }

    /// <summary>
    /// Used by tools to know where to act.
    /// </summary>
    public Transform actionPoint;


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
        new ToppledPawnState().Initialize(m_brain, m_properties, m_lookUpState);
        currentState.Enter();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        SetState(PawnStateType.Toppled);
    }

    public override void Hit(Hazard damage)
    {
        base.Hit(damage);
        m_brain.OnDamaged(damage);
        m_properties.m_physics.AddForce(damage.pushForce, ForceMode.Impulse);
    }

    protected virtual void Update()
    {
        m_brain.UpdateCommands();

        while (true)
        {
            PawnStateType nextState = currentState.Update();
            if (nextState == currentState.stateType) break;

            SetState(nextState);
        }
    }

    void SetState(PawnStateType nextState)
    {
        currentState.Exit();
        currentState = m_lookUpState[nextState];
        currentState.Enter();
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

}


/// <summary>
/// A pawn is a state machine that can have these state types.
/// </summary>
public enum PawnStateType
{
    Idle,
    Prepare,
    Attack,
    Defend,
    Sprint,
    Interact,
    Toppled,
}

public abstract class PawnState
{
    public PawnStateType stateType;
    protected Brain m_brain;
    protected PawnProperties m_properties;

    /// <summary>
    /// In a state's public constructor, define its PawnStateType!
    /// </summary>
    protected PawnState() { }

    /// <summary>
    /// Use this to set up the state properly.
    /// </summary>
    public virtual void Initialize(Brain brain, PawnProperties attributes, Dictionary<PawnStateType, PawnState> lookUpState)
    {
        m_brain = brain;
        m_properties = attributes;
        lookUpState.Add(stateType, this);
    }

    /// <summary>
    /// Called when this state is entered.
    /// </summary>
    public virtual void Enter()
    {

    }

    /// <summary>
    /// Called in the pawn's Update() and makes the pawn switch state to match its return value.
    /// First check if conditions are met to switch state, if not, do the update logic!
    /// </summary>
    /// <returns></returns>
    public virtual PawnStateType Update()
    {
        m_properties.selectedToolIndex = m_brain.commands.selected;
        if (m_brain.commands.primary)
        {
            if (m_properties.mana.currentMana > m_properties.selectedTool.GetManaCost())
            {
                m_properties.mana.currentMana -= m_properties.selectedTool.GetManaCost();
                m_properties.selectedTool.StartPrimaryAction(m_properties.actionPoint);
            }
        }
        m_properties.m_body.localRotation *= Quaternion.AngleAxis(m_brain.commands.spin * Time.deltaTime, Vector3.up);
        return this.stateType;
    }

    /// <summary>
    /// The physical behaviour of this state, called in the pawn's FixedUpdate().
    /// </summary>
    public virtual void FixedUpdate()
    {
        m_properties.m_physics.AddForce((m_properties.m_body.forward * m_brain.commands.forwards + m_properties.m_body.right * m_brain.commands.rightwards).normalized * m_properties.m_speed);
    }

    /// <summary>
    /// Called when the state is exited.
    /// </summary>
    public virtual void Exit()
    {

    }
}
