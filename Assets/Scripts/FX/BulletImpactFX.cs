using DataDeclaration;
using UnityEngine;

public class BulletImpactFX : MonoBehaviour, IBulletImpact
{
    [SerializeField] private GameObject imapctFX;
    [SerializeField] private GameObject bulletHole;

    public void InitBulletImpact(Vector3 hitPoint, Vector3 hitDirection)
    {
        transform.position = hitPoint;
        transform.rotation = Quaternion.LookRotation(hitDirection);
    }
}
