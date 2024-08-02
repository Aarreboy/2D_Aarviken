using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Brain
{
    public KeyCode forwards = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode rightwards = KeyCode.D;
    public KeyCode leftwards = KeyCode.A;
    public KeyCode primary = KeyCode.Mouse0;
    public float spin_speed = 90;
    public override void UpdateCommands()
    {
        Movement();
        Spin();
        UpdateActions();
    }

    void Movement()
    {
        commands.forwards = 0;
        commands.rightwards = 0;

        if (Input.GetKey(forwards))
            commands.forwards += 1;

        if (Input.GetKey(backwards))
            commands.forwards -= 1;

        if (Input.GetKey(rightwards))
            commands.rightwards += 1;

        if (Input.GetKey(leftwards))
            commands.rightwards -= 1;
    }

    void Spin()
    {
        commands.spin = Input.GetAxis("Mouse X") * spin_speed;
    }

    void UpdateActions()
    {
        commands.primary = Input.GetKey(primary);
    }
}
