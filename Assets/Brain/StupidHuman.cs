using UnityEngine;

public class StupidHuman : Brain
{
    Transform target;

    public override void Initialize(PawnProperties attributes)
    {
        base.Initialize(attributes);
    }

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    public override void UpdateCommands()
    {
        Movement();
        Spin();
    }

    void Movement()
    {
        commands.forwards = 0;
        commands.forwards += 1;
    }

    void Spin()
    {
        //This calculates whether the player is on the left or the right side of the "StupidHuman".
        Plane plane = new Plane(m_properties.m_body.right, transform.position);
        bool right = plane.GetDistanceToPoint(target.position) > 0;

        //If the player is to the right, spin clockwise, else....
        if (right) commands.spin = 90;
        else commands.spin = -90;
    }

}
