using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private PlayerSO selectedCharacterData;
    [SerializeField] private WeaponSO selectedWeaponData;

    protected override void Awake()
    {
        base.Awake();
        isDontDestroyOnLoad = false;
        CreatePlayer(selectedCharacterData, selectedWeaponData);
    }

    public void CreatePlayer(PlayerSO selectedCharacterData, WeaponSO selectedWeaponData)
    {
        var prefab = Resources.Load<Player>("Prefabs/Character/Player");
        Player player = Instantiate(prefab);
        player.Init(selectedCharacterData, selectedWeaponData);
    }
}
