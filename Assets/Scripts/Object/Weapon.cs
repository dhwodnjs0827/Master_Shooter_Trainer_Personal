using System;
using Cinemachine;
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
    public event Action OnShoot;

    void Awake()
    {
        maxAmmo = weaponSO.MaxAmmo;
        currentAmmo = maxAmmo;
    }

    public void Shoot()
    {
        if (currentAmmo == 0)
        {
            return;
        }
        Debug.Log("발사!");
        currentAmmo--;
        Vector3 bulletDirection = (frontSight.position - rearSight.position).normalized;
        Ray bulletRay = new Ray(frontSight.position, bulletDirection);
        if (Physics.Raycast(bulletRay, out RaycastHit hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject);
        }
        OnShoot?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Ray bulletRay = new Ray(frontSight.position, (frontSight.position - rearSight.position).normalized);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bulletRay);
    }
}
