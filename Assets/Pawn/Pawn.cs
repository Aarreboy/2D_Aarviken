using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A "living" game object. Needs a brain to function, can carry and use tools. Create a prefab variant from the prefab "Pawn" to get started on a new pawn!
/// </summary>
public class Pawn : Attackable
{
    public Brain m_brain;
    [SerializeField] PawnProperties m_properties;

    /// <summary>
    /// This will call its Updated() in Update(). Can result in a change then, or for example when this pawn takes damage.
    /// </summary>
    PawnState currentState;

    /// <summary>
    /// Contains all the pawn's states. Pass the wanted state type to get a reference to a specific state.
    /// </summary>
    Dictionary<PawnStateType, PawnState> m_lookUpState = new Dictionary<PawnStateType, PawnState>();

    protected override void Awake()
    {
        base.Awake();

        m_brain.Initialize(m_properties);

        InitializeTools();

        // Add the states
        new IdlePawnState().Initialize(m_brain, m_properties, m_lookUpState);
        new SprintPawnState().Initialize(m_brain, m_properties, m_lookUpState);
        new ToppledPawnState().Initialize(m_brain, m_properties, m_lookUpState);

        // Set the initial state
        currentState = m_lookUpState[PawnStateType.Idle];
        currentState.Enter();
    }

    void InitializeTools()
    {
        m_properties.tools = new Tool[m_properties.toolTypes.Length];
        for (int i = 0; i < m_properties.toolTypes.Length; i++)
        {
            GameObject tool = Instantiate(m_properties.toolStorage.GetTool(m_properties.toolTypes[i]), m_properties.m_body);
            m_properties.tools[i] = tool.GetComponent<Tool>();
        }
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

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //Now the pawn always becomes toppled by damage...
        SetState(PawnStateType.Toppled);
    }

    public override void Hit(Hazard damage)
    {
        base.Hit(damage);

        // alert the brain of the hazard
        m_brain.OnDamaged(damage);

        // add the push force from the hazard
        m_properties.m_physics.AddForce(damage.pushForce, ForceMode.Impulse);
    }
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
                m_properties.selectedTool.StartPrimaryAction(m_properties.actionPoint, m_brain.commands.spin);
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
