using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MagicAttack", order = 1)]
public class MagicAttack : Tool
{
    public GameObject projectilePrefab; // Drag the projectile prefab in Unity Inspector

    public override PawnStateType StartAction(Transform actionPoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, actionPoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(actionPoint.forward);
        return PawnStateType.Idle;
    }
}
