using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : Pawn
{
    void Update()
    {
        m_brain.UpdateCommands();
        m_body.localRotation *= Quaternion.AngleAxis(m_brain.commands.spin * Time.deltaTime, Vector3.up);

    }

    private void FixedUpdate()
    {
        m_physics.AddForce((m_body.forward * m_brain.commands.forwards + m_body.right * m_brain.commands.rightwards).normalized * m_speed);
    }
}
