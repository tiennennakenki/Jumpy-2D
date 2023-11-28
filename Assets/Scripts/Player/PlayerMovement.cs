using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement: SaiMonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float moveSpeed = 7f;
    [SerializeField] protected float jumpForce = 14f;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float directionX = 0f;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected BoxCollider2D coll;
    [SerializeField] protected LayerMask jumpableGround;
    [SerializeField] protected AudioSource jumpSoundEffect;

    [SerializeField] protected MovementState state;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
        this.LoadAnimator();
        this.LoadSpriteRenderer();
        this.LoadBoxCollider2D();
        this.LoadJumpableGroundD();
        this.LoadJumpSoundEffect();
    }

    protected virtual void LoadJumpableGroundD()
    {
        this.jumpableGround = 1 << LayerMask.NameToLayer("Ground");
    }

    protected virtual void LoadJumpSoundEffect()
    {
        // Lấy tất cả các AudioSource con của GameObject hiện tại
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        // Duyệt qua mảng audioSources
        foreach (AudioSource audioSource in audioSources)
        {
            // Kiểm tra nếu có AudioClip
            if (audioSource.clip.name == "jump")
            {
                // Xử lý AudioSource có AudioClip
                this.jumpSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadJumpSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.sprite != null) return;
        this.sprite = GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    protected virtual void LoadBoxCollider2D()
    {
        if (this.coll != null) return;
        this.coll = GetComponent<BoxCollider2D>();
        Debug.LogWarning(transform.name + ": LoadBoxCollider2D", gameObject);
    }

    protected override void Update()
    {
        base.Update();
        this.Moving();
        this.UpdateAnimationState();
    }

    protected virtual void Moving()
    {
        directionX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(directionX * this.moveSpeed, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, this.jumpForce);
        }
    }

    protected virtual void UpdateAnimationState()
    {
        this.RunningState();
        this.JumpingState();
        this.FallingState();
    }

    protected virtual void RunningState()
    {

        if (directionX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (directionX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        animator.SetInteger("state", (int)state);
    }

    protected virtual void JumpingState()
    {
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }

        animator.SetInteger("state", (int)state);
    }

    protected virtual void FallingState()
    {
        if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    protected bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
