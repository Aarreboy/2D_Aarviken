using UnityEngine;

public class StupidHuman : Brain
{
    Transform body;
    Transform target;
    protected override void Awake()
    {
        base.Awake();
        body = GetComponent<Pawn>().m_body;

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
        Plane plane = new Plane(body.right, transform.position);
        bool right = plane.GetDistanceToPoint(target.position) > 0;
        if (right) commands.spin = 90;
        else commands.spin = -90;
    }

}
