using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : SaiMonoBehaviour
{
    [SerializeField] protected float speed = 5f;
    public float Speed => speed;

    protected override void Update()
    {
        base.Update();
        this.Rotate();
    }

    protected virtual void Rotate()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
