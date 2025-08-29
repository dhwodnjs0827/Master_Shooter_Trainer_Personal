using System;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] protected bool isVisibleOnStart = false;
    protected bool isInitialized = false;

    public event Action<BaseUI> OnShow;
    public event Action<BaseUI> OnHide;

    public bool IsVisible => gameObject.activeSelf;

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Start()
    {
        if (!isVisibleOnStart)
        {
            Hide();
        }
    }

    protected virtual void Init()
    {
        isInitialized = true;
    }

    public virtual void Show()
    {
        if (!isInitialized)
        {
            return;
        }
        
        gameObject.SetActive(true);
        OnShow?.Invoke(this);
    }

    public virtual void Hide()
    {
        if (!isInitialized)
        {
            return;
        }
        
        gameObject.SetActive(false);
        OnHide?.Invoke(this);
    }

    public virtual void Toggle()
    {
        if (gameObject.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
