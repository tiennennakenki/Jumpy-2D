using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : SaiMonoBehaviour
{
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    [SerializeField] protected AudioSource hurtSoundEffect;
    public AudioSource HurtSoundEffect => hurtSoundEffect;
    [SerializeField] protected AudioSource deadSoundEffect;
    public AudioSource DeadSoundEffect => deadSoundEffect;
    [SerializeField] protected bool isInvincible = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadHurtSoundEffect();
        this.LoadDeadSoundEffect();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadHurtSoundEffect()
    {
        
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "enemyHurt")
            {
                this.hurtSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadHurtSoundEffect", gameObject);
            }
        }
    }

    protected virtual void LoadDeadSoundEffect()
    {

        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "enemyDeath")
            {
                this.deadSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadDeadSoundEffect", gameObject);
            }
        }
    }

    public void OnDeathAnimationComplete()
    {
        Destroy(transform.parent.gameObject);
    }

    protected virtual void DeadSoundEffectPlay()
    {
        this.deadSoundEffect.Play();
    }

    protected virtual void DisableInvincibility()
    {
        this.isInvincible = false;
    }
}
