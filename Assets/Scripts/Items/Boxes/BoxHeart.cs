using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHeart : BoxCollector
{
    [Header("Box Heart")]
    [SerializeField] protected GameObject heart;
    protected bool shatteredCrated = false;
    [SerializeField] protected int countHit = 3;

    protected override void Start()
    {
        base.Start();
        this.countHit = 3;
    }
    protected override void Update()
    {
        base.Update();
        this.DestroyedObj();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHeart();
    }

    protected virtual void LoadHeart()
    {
        if (this.heart != null) return;
        this.heart = transform.Find("Heart").gameObject;
        Debug.LogWarning(transform.name + ": LoadHeart", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            this.countHit--;
            if(this.countHit == 0)
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
