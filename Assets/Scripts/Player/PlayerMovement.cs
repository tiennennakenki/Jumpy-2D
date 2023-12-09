using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement: SaiMonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float jumpForce = 20f;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float directionX = 0f;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected BoxCollider2D coll;
    [SerializeField] protected LayerMask jumpableGround;
    [SerializeField] protected LayerMask jumpableWall;
    [SerializeField] protected AudioSource jumpSoundEffect;
    //[SerializeField] protected float wallJumpCoolDown;

    //JumpableWall
    protected bool isWallJumpable = true;
    protected float wallJumpingDirection;
    protected float wallJumpingTime = 0.2f;
    [SerializeField] protected float wallJumpingCounter;
    protected float wallJumpingDuration = 0.4f;
    protected Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] protected bool isWallSliding;
    [SerializeField] protected float wallSlidingSpeed = 2f;

    [SerializeField] protected MovementState state;

    [SerializeField] protected bool doubleJumpable;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
        this.LoadAnimator();
        this.LoadSpriteRenderer();
        this.LoadBoxCollider2D();
        this.LoadJumpableGround();
        this.LoadJumpableWall();
        this.LoadJumpSoundEffect();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.rb.gravityScale = 6;
        this.rb.freezeRotation = true;
    }

    protected virtual void LoadJumpableGround()
    {
        this.jumpableGround = 1 << LayerMask.NameToLayer("Ground");
    }

    protected virtual void LoadJumpableWall()
    {
        this.jumpableWall = 1 << LayerMask.NameToLayer("Wall");
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

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            this.doubleJumpable = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                GroundJump();
            }
            else if (this.doubleJumpable)
            {
                DoubleJump();
            }
            //rb.velocity = new Vector2(directionX * this.moveSpeed, rb.velocity.y);
            else if (IsWalled() && !IsGrounded() && this.directionX != 0f) WallJump();
        }

        this.WallSlide();
    }

    protected virtual void GroundJump()
    {
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, this.jumpForce);

        this.doubleJumpable = !this.doubleJumpable;
    }

    protected virtual void DoubleJump()
    {
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, this.jumpForce);

        this.animator.SetTrigger("doubleJump");

        this.doubleJumpable = !this.doubleJumpable;
    }

    protected virtual void UpdateAnimationState()
    {
        this.RunningState();
        this.JumpingState();
        this.FallingState();
        this.WallSlidingState();
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

    protected virtual void WallSlidingState()
    {
        if(IsWalled() && !IsGrounded())
        {
            if (rb.velocity.x < -.1f)
            {
                state = MovementState.wallSliding;
                sprite.flipX = true;
            }
            if (rb.velocity.x > .1f)
            {
                state = MovementState.wallSliding;
                sprite.flipX = false;
            }

            animator.SetInteger("state", (int)state);
        }
    }

    protected bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    protected bool IsWalled()
    {
        // Check for collision on the left side
        bool isWalledLeft = Physics2D.BoxCast(coll.bounds.min, coll.bounds.size, 0f, Vector2.left, .1f, jumpableWall);

        // Check for collision on the right side
        bool isWalledRight = Physics2D.BoxCast(coll.bounds.max, coll.bounds.size, 0f, Vector2.right, .1f, jumpableWall);

        // Return true if either side is walled
        return isWalledLeft || isWalledRight;
    }


    protected virtual void WallSlide()
    {
        if(IsWalled() && !IsGrounded() && this.directionX != 0f)
        {
            this.isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -this.wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            this.isWallSliding = false;
        }
    }

    protected virtual void WallJump()
    {

        if (this.isWallSliding)
        {
            this.wallJumpingDirection = -transform.localScale.x;
            this.wallJumpingCounter = this.wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            this.wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump") && this.wallJumpingCounter > 0 && this.isWallJumpable)
        {
            this.isWallJumpable = false;
            rb.velocity = new Vector2(this.wallJumpingDirection * this.wallJumpingPower.x, this.wallJumpingPower.y);
            
            this.wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                Vector2 localScale = transform.localScale;

                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDirection);
        }
    }

    protected virtual void StopWallJumping()
    {
        this.isWallJumpable = true;
    }
}
