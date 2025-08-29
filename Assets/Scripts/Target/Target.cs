using DataDeclaration;
using UnityEngine;

public class Target : MonoBehaviour, IBulletImpact, IDamageable
{
    private float maxHp;
    private float currentHp;

    public void InitBulletImpact(Vector3 hitPoint, Vector3 hitDirection)
    {
        var resource = Resources.Load<BulletImpactFX>("FX/BulletImpactFX_Metal");
        var bulletImpact = Instantiate(resource);
        bulletImpact.InitBulletImpact(hitPoint, hitDirection);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        float finalDamage = damageInfo.HitPart.Equals(BodyPartType.Body) ? damageInfo.Damage : damageInfo.Damage * 1.5f;
        currentHp -= finalDamage;
        InitBulletImpact(damageInfo.HitPoint, damageInfo.HitDirection);
    }
}
