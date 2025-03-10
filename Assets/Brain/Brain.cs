using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Commands
{
    public sbyte forwards, rightwards;
    public bool primary;
    public float spin;
}
public abstract class Brain : MonoBehaviour
{
    public Commands commands;
    protected PawnAttributes m_attributes;

    public virtual void Initialize(PawnAttributes attributes)
    {
        ZeroCommands();
        m_attributes = attributes;
    }

    public virtual void ZeroCommands()
    {
        commands.forwards = 0;
        commands.rightwards = 0;
        commands.primary = false;
        commands.spin = 0;
    }

    public virtual void UpdateCommands()
    {

    }

    public virtual void OnDamaged(float damage)
    {

    }
}