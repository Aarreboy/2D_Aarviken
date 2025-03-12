using UnityEngine;

public class MagicAttack : Tool
{
    public GameObject projectilePrefab; // Drag the projectile prefab in Unity Inspector

    //Get the primary action to create a projectile
    public override PawnStateType StartPrimaryAction(Transform actionPoint, float startDirection)
    {
        GameObject projectile = Instantiate(projectilePrefab, actionPoint.position, actionPoint.rotation);
        projectile.GetComponent<Projectile>().Initialize();
        return PawnStateType.Idle;
    }
}
