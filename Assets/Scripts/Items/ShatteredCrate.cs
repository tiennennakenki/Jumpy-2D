using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredCrate : SaiMonoBehaviour
{
    [SerializeField] protected Rigidbody2D[] childrenRbs;
    protected float randomTorque, randomDirX, randomDirY;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChildrenRbs();
    }

    protected virtual void LoadChildrenRbs()
    {
        this.childrenRbs = GetComponentsInChildren<Rigidbody2D>();

        foreach(Rigidbody2D rigidbody2D in childrenRbs)
        {
            this.randomTorque = Random.Range(-100f, 100f);
            this.randomDirX = Random.Range(-200f, 200f);
            this.randomDirY = Random.Range(-200f, 200f);
            rigidbody2D.AddTorque(this.randomTorque);
            rigidbody2D.AddForce(new Vector2(randomDirX, randomDirY));
            Destroy(gameObject, 2f);
        }
    }
}
