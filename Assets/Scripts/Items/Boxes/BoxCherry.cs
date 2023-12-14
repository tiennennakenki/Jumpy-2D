using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCherry : BoxCollector
{
    [Header("Box Cherry")]
    [SerializeField] protected GameObject cherry;
    protected bool shatteredCrated = false;
    [SerializeField] protected int countHit = 1;
    protected override void Update()
    {
        base.Update();
        this.DestroyedObj();
    }

    protected override void Start()
    {
        base.Start();
        this.countHit = 1;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCherry();
    }

    protected virtual void LoadCherry()
    {
        if (this.cherry != null) return;
        this.cherry = transform.Find("Cherry").gameObject;
        Debug.LogWarning(transform.name + ": LoadCherry", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.countHit--;
            if (this.countHit == 0)
            {
                Invoke("DestroyShatteredCrate", .2f);
            }
        }
    }

    public override void DestroyShatteredCrate()
    {
        Destroy(crate);
        if (!shatteredCrated)
        {
            shatteredCrated = true;
            this.shatteredCrate.gameObject.SetActive(true);
            this.LoadSoundShatteredCrateEffect();
            soundShatteredCrateEffect.Play();
            Instantiate(shatteredCrate, transform.position, Quaternion.identity);
        }
    }
}
