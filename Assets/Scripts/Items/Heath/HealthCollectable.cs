using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : SaiMonoBehaviour
{
    [SerializeField] protected float healthValue = 1;
    public float HealthValue => healthValue;

    [SerializeField] protected AudioSource addHealthSoundEffect;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAddHealthSoundEffect();
    }

    protected virtual void LoadAddHealthSoundEffect()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "addHealth")
            {
                this.addHealthSoundEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadAddHealthSoundEffect", gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerLife>().AddHealth(healthValue);
            this.addHealthSoundEffect.Play();
            gameObject.SetActive(false);
            Destroy(transform.gameObject);
        }
    }
}
