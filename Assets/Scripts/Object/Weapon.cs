using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform frontSight;
    [SerializeField] private Transform rearSight;

    [SerializeField] private GameObject muzzleFX;

    private int maxAmmo;
    private int currentAmmo;
    private float damage;

    public Transform CameraPoint => cameraPoint;
    public event Action<float> OnRecoil;

    public void Init(WeaponSO selectedWeaponData)
    {
        damage = selectedWeaponData.Damage;
        maxAmmo = selectedWeaponData.MaxAmmo;
        currentAmmo = maxAmmo;
    }

    public void Shoot(float recoilValue)
    {
        if (currentAmmo == 0)
        {
            Debug.Log("재장전 필요!");
            return;
        }
        currentAmmo--;
        Vector3 bulletDirection = (frontSight.position - rearSight.position).normalized;
        Ray bulletRay = new Ray(frontSight.position, bulletDirection);
        if (Physics.Raycast(bulletRay, out RaycastHit hitInfo))
        {
            Debug.Log($"{hitInfo.collider.gameObject.name}에 {damage} 데미지");
        }
        Instantiate(muzzleFX, muzzlePoint);
        OnRecoil?.Invoke(recoilValue);
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }

    private void OnDrawGizmos()
    {
        Ray bulletRay = new Ray(frontSight.position, (frontSight.position - rearSight.position).normalized);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bulletRay);
    }
}
