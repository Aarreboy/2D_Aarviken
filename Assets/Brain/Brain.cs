using UnityEngine;

public struct Commands
{
    public sbyte forwards, rightwards;
    public byte selected;
    public bool primary, secondary, tertiary;
    public bool primaryHold, secondaryHold, tertiaryHold;
    public bool dash;
    public bool sprint;
    public float spin;
}
public abstract class Brain : MonoBehaviour
{
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

    public virtual void OnDamaged(float damage)
    {

    }

    public bool IsTryingToMove()
    {
        return new Vector3(commands.forwards, commands.rightwards).magnitude > 0;
    }
}