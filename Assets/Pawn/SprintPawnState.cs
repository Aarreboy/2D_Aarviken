using UnityEngine;

public class SprintPawnState : PawnState
{
    public SprintPawnState()
    {
        stateType = PawnStateType.Sprint;
    }


    public override void Enter()
    {
        m_properties.m_physics.linearDamping /= 2;
    }

    public override PawnStateType Update()
    {
        if (!m_brain.commands.sprint || !m_brain.IsTryingToMove())
        {
            return PawnStateType.Idle;
        }

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

    public override void FixedUpdate()
    {
        m_properties.m_physics.AddForce((m_properties.m_body.forward * m_brain.commands.forwards + m_properties.m_body.right * m_brain.commands.rightwards).normalized * m_properties.m_speed);
    }

    public override void Exit()
    {
        m_properties.m_physics.linearDamping *= 2;
    }
}
