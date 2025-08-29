using UnityEngine;

public interface IBodyPartDamageReceiver
{
    public void ReceiveBodyPartDamage(float damage, Vector3 hitPoint, Vector3 hitDirection);
}
