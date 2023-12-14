using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BoxCollector : ItemsManager
{
    [SerializeField] protected GameObject shatteredCrate, crate;
    [SerializeField] protected AudioSource soundShatteredCrateEffect;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShatteredCrate();
        this.LoadCrate();
        this.LoadSoundShatteredCrateEffect();
    }

    protected virtual void LoadShatteredCrate()
    {
        if (this.shatteredCrate != null) return;
        this.shatteredCrate = transform.Find("Shattered Crate").gameObject;
        Debug.LogWarning(transform.name + ": LoadShatteredCrate", gameObject);
    }

    protected virtual void LoadCrate()
    {
        if (this.crate != null) return;
        this.crate = transform.Find("Crate").gameObject;
        Debug.LogWarning(transform.name + ": LoadCrate", gameObject);
    }

    protected virtual void LoadSoundShatteredCrateEffect()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "shatteredCrate")
            {
                this.soundShatteredCrateEffect = audioSource;
                Debug.LogWarning(transform.name + ": LoadSoundShatteredCrateEffect", gameObject);
            }
        }
    }
}
