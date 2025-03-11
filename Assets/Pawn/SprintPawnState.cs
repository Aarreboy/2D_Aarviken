using UnityEngine;

public class SprintPawnState : PawnState
{
    public SprintPawnState()
    {
        stateType = PawnStateType.Sprint;
    }


    public override void Enter()
    {
        //Reduce the friction to make the able to accelerate to a higher speed.
        m_properties.m_physics.linearDamping /= 2;
    }

    public override PawnStateType Update()
    {
        //Go back to idle state if sprint input no longer applies.
        //Can currently sprint in all directions, but maybe we just want forward...
        if (!m_brain.commands.sprint || !m_brain.IsTryingToMove())
        {
            return PawnStateType.Idle;
        }

        //Can still change the selected tool during sprint.
        m_properties.selectedToolIndex = m_brain.commands.selected;

        //Can still use a tool during sprint, however, we might want to implement a SprintUse() for different behaviour!
        if (m_brain.commands.primary)
        {
            if (m_properties.mana.currentMana > m_properties.selectedTool.GetManaCost())
            {
                m_properties.mana.currentMana -= m_properties.selectedTool.GetManaCost();
                m_properties.selectedTool.StartPrimaryAction(m_properties.actionPoint);
            }
        }

        //Might want to override sprint rotation behaviour later...
        m_properties.m_body.localRotation *= Quaternion.AngleAxis(m_brain.commands.spin * Time.deltaTime, Vector3.up);
        
        return this.stateType;
    }

    public override void FixedUpdate()
    {
        m_properties.m_physics.AddForce((m_properties.m_body.forward * m_brain.commands.forwards +
            m_properties.m_body.right * m_brain.commands.rightwards).normalized * 
            m_properties.m_speed);
    }

    public override void Exit()
    {
        //Return the friction to normal.
        m_properties.m_physics.linearDamping *= 2;
    }
}
