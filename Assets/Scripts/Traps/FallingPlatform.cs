using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : ItemsManager
{
    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;
    [SerializeField] protected BoxCollider2D boxCollider2D;
    public BoxCollider2D BoxCollider2D => boxCollider2D;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
        this.LoadAnimator();
        this.LoadBoxCollider2D();
    }

    protected override void Awake()
    {
        base.Awake();
        this.rb.gravityScale = 0f;
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        this.rb =  transform.GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + "LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + "LoadAnimator", gameObject);
    }

    protected virtual void LoadBoxCollider2D()
    {
        if (this.boxCollider2D != null) return;
        this.boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Debug.LogWarning(transform.name + "LoadBoxCollider2D", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.animator.SetTrigger("isOn");
            this.rb.gravityScale = 6f;
            this.boxCollider2D.isTrigger = true;
            Invoke("DestroyedObj", 2f);
        }
    }
}
