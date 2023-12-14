using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStar : BoxCollector
{
    [Header("Box Star")]
    [SerializeField] protected GameObject star;
    protected bool shatteredCrated = false;
    [SerializeField] protected int countHit = 2;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStar();
    }

    protected virtual void LoadStar()
    {
        if (this.star != null) return;
        this.star = transform.Find("Star").gameObject;
        Debug.LogWarning(transform.name + ": LoadStar", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.countHit = 2;
    }
    protected override void Update()
    {
        base.Update();
        this.DestroyedObj();
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
