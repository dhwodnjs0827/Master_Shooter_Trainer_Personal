using DataDeclaration;
using UnityEngine;

public class BodyPart : MonoBehaviour, IBodyPartDamageReceiver
{
    [Header("Body Setting")]
    [SerializeField] private BodyPartType bodyType;

    private IDamageable damageable;

    private void Awake()
    {
        damageable = GetComponentInParent<IDamageable>();
    }

    public void ReceiveBodyPartDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        var damageInfo = new DamageInfo(damage, bodyType, hitPoint, hitDirection);
        damageable.TakeDamage(damageInfo);
    }
}
