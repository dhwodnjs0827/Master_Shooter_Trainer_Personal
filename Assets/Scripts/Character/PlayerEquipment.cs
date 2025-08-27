using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private Transform weaponPos;
    private Weapon weapon;

    public Weapon EquipWeapon(WeaponSO selectedWeaponData)
    {
        var prefab = Resources.Load<Weapon>($"Prefabs/Weapon/{selectedWeaponData.ID}");
        weapon = Instantiate(prefab, weaponPos);
        weapon.Init(selectedWeaponData);
        return weapon;
    }
}
