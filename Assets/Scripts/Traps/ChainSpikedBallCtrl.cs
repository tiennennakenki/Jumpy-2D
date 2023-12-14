using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSpikedBallCtrl : SaiMonoBehaviour
{
    [SerializeField] protected GameObject chain;
    public GameObject Chain => chain;
    [SerializeField] protected GameObject spikedBall;
    public GameObject SpikedBall => spikedBall;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChain();
        this.LoadSpikedBall();
    }

    protected virtual void LoadChain()
    {
        if (this.chain != null) return;
        this.chain = transform.Find("Chain").gameObject;
        Debug.LogWarning(transform.name + ": LoadMoveCircle", gameObject);
    }

    protected virtual void LoadSpikedBall()
    {
        if (this.spikedBall != null) return;
        this.spikedBall = transform.Find("Spiked Ball").gameObject;
        Debug.LogWarning(transform.name + ": LoadSpikedBall", gameObject);
    }
}
