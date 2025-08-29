using System;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private bool canReload = true;
    public event Action OnReloadComplete;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Reload()
    {
        if (canReload)
        {
            canReload = false;
            animator.SetTrigger("Reload");
        }
    }

    // Player_Reload 애니메이션 클립 끝에 이벤트로 등록됨
    private void ReloadComplete()
    {
        canReload = true;
        OnReloadComplete?.Invoke();
    }
}
