using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public static void ToggleMouseCursor(bool isActivation)
    {
        Cursor.lockState = isActivation ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActivation;
    }
}
