using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : SaiMonoBehaviour
{
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
        this.animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + " :LoadAnimator", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.animator.SetTrigger("havePlayer");
        }
    }
}
