public class PlayerStatHandler
{
    private float speed;
    private float step;
    private float recoil;
    private float handling;
    
    public float Speed => speed;
    public float Step => step;
    public float Recoil => recoil;
    public float Handling => handling;
    
    public PlayerStatHandler(PlayerSO data)
    {
        speed = data.Speed;
        step = data.Step;
        recoil = data.Recoil;
        handling = data.Handling;
    }
}
