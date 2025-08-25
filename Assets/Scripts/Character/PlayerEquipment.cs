using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private Transform weaponPos;
    private Weapon weapon;

    public Weapon Weapon => weapon;

    public void EquipWeapon()
    {
        var prefab = Resources.Load<Weapon>("Prefabs/Weapon/Pistol");
        weapon = Instantiate(prefab, weaponPos);
    }
}
