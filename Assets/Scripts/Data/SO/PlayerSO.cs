using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Data/Player")]
public class PlayerSO : ScriptableObject
{
    public float Speed;
    public float Step;
    public float Recoil;
    public float Handling;
}
