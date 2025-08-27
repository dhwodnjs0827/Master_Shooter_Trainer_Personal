using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private Transform weaponPos;
    private Weapon weapon;

    public Weapon Weapon => weapon;

    public Weapon EquipWeapon()
    {
        var prefab = Resources.Load<Weapon>("Prefabs/Weapon/W0001");
        weapon = Instantiate(prefab, weaponPos);
        return weapon;
    }
}
