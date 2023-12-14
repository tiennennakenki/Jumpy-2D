using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircle : SaiMonoBehaviour
{
    [Header("Move Circle")]
    [SerializeField] protected Transform centerPoint;
    [SerializeField] protected float speed = 2f;
    public float Speed => speed;
    [SerializeField] protected float distance = 4f; 
    public float Distance => distance;

    [SerializeField] protected ChainSpikedBallCtrl chainSpikedBallCtrl;
    public ChainSpikedBallCtrl ChainSpikedBallCtrl => chainSpikedBallCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChainSpikedBallCtrl();
        this.LoadCenterPoint();
    }

    protected virtual void LoadChainSpikedBallCtrl()
    {
        if (this.chainSpikedBallCtrl != null) return;
        this.chainSpikedBallCtrl = transform.GetComponentInParent<ChainSpikedBallCtrl>();
        Debug.LogWarning(transform.name + ": LoadChainSpikedBallCtrl", gameObject);
    }

    protected virtual void LoadCenterPoint()
    {
        if (this.centerPoint != null) return;
        this.centerPoint = chainSpikedBallCtrl.transform.Find("Chain");
        Debug.LogWarning(transform.name + ": LoadCenterPoint", gameObject);
    }

    protected override void Update()
    {
        base.Update();
        this.Moving();
    }

    protected virtual void Moving()
    {
        Vector3 offset = new Vector3(Mathf.Sin(Time.time * speed), Mathf.Cos(Time.time * speed), 0f);
        Vector3 newPosition = centerPoint.position + offset * distance;


        transform.position = newPosition;
    }
}

