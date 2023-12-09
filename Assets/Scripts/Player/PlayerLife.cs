using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : SaiMonoBehaviour
{
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    [SerializeField] protected AudioSource deathSoundEffect;

    [SerializeField] protected AudioSource hurtSoundEffect;

    [SerializeField] protected float startingHeath = 3;
    public float StartingHeath => startingHeath;

    [SerializeField] protected float currentHealth;
    public float CurrentHeath => currentHealth;

    [SerializeField] protected bool isInvincible = false;

    protected override void Awake()
    {
        base.Awake();
        this.currentHealth = this.startingHeath;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadRigidbody2D();
        this.LoadDeathSoundEffect();
        this.LoadHurtSoundEffect();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + " :LoadAnimator", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + " :LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadDeathSoundEffect()
    {
        // Lấy tất cả các AudioSource con của GameObject hiện tại
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        // Duyệt qua mảng audioSources
        foreach (AudioSource audioSource in audioSources)
        {
            // Kiểm tra nếu có AudioClip
            if (audioSource.clip.name == "death")
            {
                // Xử lý AudioSource có AudioClip
                this.deathSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadDeathSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadHurtSoundEffect()
    {
        // Lấy tất cả các AudioSource con của GameObject hiện tại
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        // Duyệt qua mảng audioSources
        foreach (AudioSource audioSource in audioSources)
        {
            // Kiểm tra nếu có AudioClip
            if (audioSource.clip.name == "hurt")
            {
                // Xử lý AudioSource có AudioClip
                this.hurtSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadHurtSoundEffect", gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            this.TakeDamge(1);
        }
        
    }

    protected virtual void Die()
    {
        this.deathSoundEffect.Play();
        this.rb.bodyType = RigidbodyType2D.Static;
        this.animator.SetTrigger("death");
    }

    protected virtual void Hurt()
    {
        if (!isInvincible)
        {
            this.isInvincible = true;
            this.hurtSoundEffect.Play();
            this.animator.SetTrigger("hurt");

            Invoke("DisableInvincibility", 0.5f);
        }
    }

    protected virtual void DisableInvincibility()
    {
        this.isInvincible = false;
    }

    protected virtual void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected virtual void TakeDamge(float _damge)
    {
        if (this.isInvincible) return;
        this.currentHealth = Mathf.Clamp(this.currentHealth - _damge, 0, startingHeath);
        if (this.currentHealth > 0)
        {
            this.Hurt();
        }
        else
        {
            this.Die();
        }
    }

    public virtual void AddHealth(float value)
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth + value, 0, startingHeath);
    }
}
