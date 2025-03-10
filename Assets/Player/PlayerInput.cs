using UnityEngine;

public class PlayerInput : Brain
{
    public KeyCode forwards = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode rightwards = KeyCode.D;
    public KeyCode leftwards = KeyCode.A;
    public KeyCode primary = KeyCode.Mouse0;
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
        commands.primary = Input.GetKeyDown(primary);

        if (Input.GetKeyDown(KeyCode.Alpha1)) commands.selected = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) commands.selected = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) commands.selected = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) commands.selected = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) commands.selected = 4;

        if ((byte)(m_properties.tools.Length -1) < commands.selected) commands.selected = (byte)(m_properties.tools.Length - 1);

    }

    public override void OnDamaged(float damage)
    {
        healthBar.UpdateHealthBar();
    }
}