using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainRenderer : ChainSpikedBallCtrl
{
    [SerializeField] protected LineRenderer line;
    public LineRenderer Line => line;
    protected override void FixedUpdate()
    {
        base.Update();
        this.CreateLine();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLineRenderer();
    }

    protected virtual void LoadLineRenderer()
    {
        if (this.line != null) return;
        this.line = transform.GetComponent<LineRenderer>();
        Debug.LogWarning(transform.name + ": LoadLineRenderer", gameObject);
    }

    protected virtual void CreateLine()
    {
        this.line.SetPosition(0, this.Chain.transform.position);
        this.line.SetPosition(1, this.SpikedBall.transform.position);
    }
}
