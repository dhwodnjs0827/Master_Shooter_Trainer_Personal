using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform screenUIParent;
    [SerializeField] private Transform popupUIParent;
    
    private Dictionary<Type, BaseUI> activeUIDict = new Dictionary<Type, BaseUI>();
    private Dictionary<Type, BaseUI> uiPrefabs = new Dictionary<Type, BaseUI>();

    /// <summary>
    /// UI 표시
    /// <para>UI가 없으면 생성 후 표시</para>
    /// </summary>
    public T ShowUI<T>() where T : BaseUI
    {
        var ui = GetUI<T>();
        if (ui == null)
        {
            ui = CreateUI<T>();
        }

        if (ui != null)
        {
            ui.Show();
        }
        
        return ui;
    }

    /// <summary>
    /// UI 생성
    /// </summary>
    public T CreateUI<T>() where T : BaseUI
    {
        Type uiType = typeof(T);

        if (activeUIDict.TryGetValue(uiType, out BaseUI activeUI))
        {
            return (T)activeUI;
        }
        
        BaseUI prefab = GetUIPrefab<T>();
        Transform parent = GetUIParent(uiType);
        
        var ui = Instantiate(prefab, parent);
        activeUIDict[uiType] = ui;
        
        return (T)ui;
    }

    private BaseUI GetUIPrefab<T>() where T : BaseUI
    {
        Type uiType = typeof(T);

        if (uiPrefabs.TryGetValue(uiType, out BaseUI uiPrefab))
        {
            return uiPrefab;
        }

        string resourcePath = GetUIResourcePath<T>();
        BaseUI prefab = Resources.Load<BaseUI>(resourcePath);
        uiPrefabs[uiType] = prefab;
        return prefab;
    }

    private string GetUIResourcePath<T>() where T : BaseUI
    {
        Type uiType = typeof(T);
        return $"Prefabs/UI/{uiType.Name}";
    }

    private Transform GetUIParent(Type uiType)
    {
        if (uiType == typeof(ScreenUI))
        {
            return screenUIParent;
        }
        else
        {
            return popupUIParent;
        }
    }

    /// <summary>
    /// 활성화 된 UI 가져오기
    /// </summary>
    public T GetUI<T>() where T : BaseUI
    {
        Type uiType = typeof(T);

        if (activeUIDict.TryGetValue(uiType, out BaseUI ui))
        {
            return (T)ui;
        }
        
        return null;
    }

    /// <summary>
    /// UI 숨김
    /// </summary>
    public void HideUI<T>() where T : BaseUI
    {
        var ui = GetUI<T>();
        ui?.Hide();
    }

    /// <summary>
    /// UI 제거
    /// </summary>
    public void DestroyUI<T>() where T : BaseUI
    {
        Type uiType = typeof(T);

        if (activeUIDict.TryGetValue(uiType, out BaseUI ui))
        {
            activeUIDict.Remove(uiType);
            if (ui != null)
            {
                Destroy(ui.gameObject);
            }
        }
    }

    /// <summary>
    /// UI 존재 확인
    /// </summary>
    public bool IsUIActive<T>() where T : BaseUI
    {
        return activeUIDict.ContainsKey(typeof(T));
    }

    /// <summary>
    /// UI 활성화 여부 확인
    /// </summary>
    public bool IsUIVisible<T>() where T : BaseUI
    {
        var ui = GetUI<T>();
        return ui != null && ui.IsVisible;
    }

    public static void ToggleMouseCursor(bool isActivation)
    {
        Cursor.lockState = isActivation ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActivation;
    }
}
