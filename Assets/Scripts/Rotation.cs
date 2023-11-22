using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Rotation : SaiMonoBehaviour
{
    [SerializeField] protected float speed = 2f;
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
