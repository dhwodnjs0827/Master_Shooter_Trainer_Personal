using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class WeaponSO : ScriptableObject
{
    public float Damage;
    public int MaxAmmo;
}
