using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStar : SaiMonoBehaviour
{
    [SerializeField] protected GameObject star, shatteredCrate, crate;
    [SerializeField] protected AudioSource soundShatteredCrateEffect;
    protected bool shatteredCrated = false;
    [SerializeField] protected int countHit;

    protected override void Start()
    {
        base.Start();
        this.countHit = 2;
    }
    protected override void Update()
    {
        base.Update();
        this.DestroyedBox();
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

    public virtual void DestroyShatteredCrate()
    {
        Destroy(crate);
        if (!shatteredCrated)
        {
            shatteredCrated = true;
            soundShatteredCrateEffect.Play();
        }
        this.shatteredCrate.gameObject.SetActive(true);
        this.star.gameObject.SetActive(true);
        Instantiate(shatteredCrate, transform.position, Quaternion.identity);
    }

    protected virtual void DestroyedBox()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject); // Use gameObject instead of transform.gameObject
        }
    }
}
