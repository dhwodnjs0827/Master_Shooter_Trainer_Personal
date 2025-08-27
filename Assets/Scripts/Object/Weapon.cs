using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private Transform model;
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private Transform frontSight;
    [SerializeField] private Transform rearSight;

    private int maxAmmo;
    private int currentAmmo;

    public Transform CameraPoint => cameraPoint;
    public event Action<float> OnRecoil;

    void Awake()
    {
        maxAmmo = weaponSO.MaxAmmo;
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
            Debug.Log($"{hitInfo.collider.gameObject.name}에 {weaponSO.Damage} 데미지");
        }
        OnRecoil?.Invoke(recoilValue);
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
        Debug.Log("재장전!");
    }

    private void OnDrawGizmos()
    {
        Ray bulletRay = new Ray(frontSight.position, (frontSight.position - rearSight.position).normalized);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bulletRay);
    }
}
