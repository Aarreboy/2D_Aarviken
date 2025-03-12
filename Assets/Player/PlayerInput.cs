using UnityEngine;

/// <summary>
/// Contains input and GUI logic for the player pawn.
/// </summary>
public class PlayerInput : Brain
{
    public KeyCode forwards = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode rightwards = KeyCode.D;
    public KeyCode leftwards = KeyCode.A;
    public KeyCode primary = KeyCode.Mouse0;
    public KeyCode secondary = KeyCode.Mouse1;
    public KeyCode tertiary = KeyCode.Mouse4;
    public KeyCode dash = KeyCode.Space;
    public KeyCode sprint = KeyCode.LeftShift;
    public float spin_speed = 90;

    public PlayerHealth healthBar;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        healthBar = GetComponent<PlayerHealth>();
    }

    public override void UpdateCommands()
    {
        Shader.SetGlobalVector("PlayerPosition", transform.position);
        base.UpdateCommands();
        Movement();
        Spin();
        UpdateActions();
    }

    void Movement()
    {
        //Expects the ZeroCommands() to have been called before this.

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
        commands.primary = Input.GetKeyDown(primary);
        commands.secondary = Input.GetKeyDown(secondary);
        commands.tertiary = Input.GetKeyDown(secondary);

        commands.primaryHold = Input.GetKey(primary);
        commands.secondaryHold = Input.GetKey(secondary);
        commands.tertiaryHold = Input.GetKey(secondary);

        commands.dash = Input.GetKey(dash);
        commands.sprint = Input.GetKey(sprint);

        if (Input.GetKeyDown(KeyCode.Alpha1)) commands.selected = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) commands.selected = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) commands.selected = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) commands.selected = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) commands.selected = 4;

        if ((byte)(m_properties.tools.Length -1) < commands.selected) commands.selected = (byte)(m_properties.tools.Length - 1);

    }

    public override void OnDamaged(Hazard damage)
    {
        healthBar.UpdateHealthBar();
    }
}