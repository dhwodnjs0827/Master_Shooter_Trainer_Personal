using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class WeaponSO : ScriptableObject
{
    public string ID;
    public string Name;
    public string Description;
    public float Recoil;
    public float Damage;
    public float ReloadTime;
    public int MaxAmmo;
}
