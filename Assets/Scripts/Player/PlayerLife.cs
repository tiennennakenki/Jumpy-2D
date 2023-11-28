using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : SaiMonoBehaviour
{
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    [SerializeField] protected AudioSource deathSoundEffect;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadRigidbody2D();
        this.LoadDeathSoundEffect();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            this.Die();
        }
        
    }

    protected virtual void Die()
    {
        this.deathSoundEffect.Play();
        this.rb.bodyType = RigidbodyType2D.Static;
        this.animator.SetTrigger("death");
    }

    protected virtual void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
