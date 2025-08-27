using System;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private bool canReload;
    public event Action OnReloadComplete;

    void Awake()
    {
        animator = GetComponent<Animator>();
        canReload = true;
    }

    public void Reload()
    {
        if (canReload)
        {
            canReload = false;
            animator.SetTrigger("Reload");
        }
    }

    private void ReloadComplete()
    {
        canReload = true;
        OnReloadComplete?.Invoke();
    }
}
