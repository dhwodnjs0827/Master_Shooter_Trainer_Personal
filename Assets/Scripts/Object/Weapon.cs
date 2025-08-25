using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private CinemachineVirtualCamera adsCamera;

    public CinemachineVirtualCamera ADSCamera => adsCamera;

    public void Shoot()
    {
        Debug.Log("발사!");
    }
}
