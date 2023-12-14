using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsManager : SaiMonoBehaviour
{
    [Header("Traps Manager")]
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }
}
