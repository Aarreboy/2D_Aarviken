using UnityEngine;

public class MeleeWeapon : Tool
{
    public MeleeSwing swing;
    public override PawnStateType StartPrimaryAction(Transform actionPoint, float startDirection)
    {
        swing.StartSwing();
        return PawnStateType.Idle;
    }
}
