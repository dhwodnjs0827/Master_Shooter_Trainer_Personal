using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private Transform weaponPos;

    public Weapon EquipWeapon(WeaponSO selectedWeaponData)
    {
        var prefab = Resources.Load<Weapon>($"Prefabs/Weapon/{selectedWeaponData.ID}");
        var weapon = Instantiate(prefab, weaponPos);
        weapon.Init(selectedWeaponData);
        return weapon;
    }
}
