using UnityEngine;

public struct Commands
{
    /// <summary>
    /// Should only be -1, 0 and 1.
    /// </summary>
    public sbyte forwards, rightwards;

    /// <summary>
    /// For toolbar selection.
    /// </summary>
    public byte selected;
    /// <summary>
    /// Each tool can have up to three actions (more if how they react in different pawn states are counted).
    /// Only returns true the frame it was pressed.
    /// </summary>
    public bool primary, secondary, tertiary;

    /// <summary>
    /// Each tool can have up to three actions (more if how they react in different pawn states are counted).
    /// </summary>
    public bool primaryHold, secondaryHold, tertiaryHold;

    /// <summary>
    /// How much the pawn should rotate.
    /// </summary>
    public float spin;

    public bool dash;
    public bool sprint;
}

/// <summary>
/// That which controls a pawn.
/// </summary>
public abstract class Brain : MonoBehaviour
{
    //Should maybe have this private and only send out a copy with Get so that other's can't alter something's commands.
    public Commands commands;
    protected PawnProperties m_properties;

    public virtual void Initialize(PawnProperties attributes)
    {
        ZeroCommands();
        m_properties = attributes;
    }

    public virtual void UpdateCommands()
    {
        ZeroCommands();
    }

    /// <summary>
    /// Most commands are intended to be set every frame, and should be set to default before that.
    /// </summary>
    public virtual void ZeroCommands()
    {
        commands.forwards = 0;
        commands.rightwards = 0;
        commands.primary = false;
        commands.secondary = false;
        commands.tertiary = false;
        commands.primaryHold = false;
        commands.secondaryHold = false;
        commands.tertiaryHold = false;
        commands.dash = false;
        commands.sprint = false;
        commands.spin = 0;
    }

    /// <summary>
    /// GUI and AI should be able to react to when the pawn is hit.
    /// </summary>
    /// <param name="damage"></param>
    public virtual void OnDamaged(Hazard damage)
    {

    }

    public bool IsTryingToMove()
    {
        return new Vector3(commands.forwards, commands.rightwards).magnitude > 0;
    }
}