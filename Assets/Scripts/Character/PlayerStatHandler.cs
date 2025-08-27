using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float speed;
    [SerializeField, Range(0f, 100f)] private float step;
    [SerializeField, Range(0f, 100f)] private float recoil;
    [SerializeField, Range(0f, 100f)] private float handling;
    
    public float Speed => speed;
    public float Step => step;
    public float Recoil => recoil;
    public float Handling => handling;
    
    public void Init(PlayerSO data)
    {
        speed = data.Speed;
        step = data.Step;
        recoil = data.Recoil;
        handling = data.Handling;
    }
}
