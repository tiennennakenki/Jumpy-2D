using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : SaiMonoBehaviour
{
    [SerializeField] protected PlayerLife playerLife;
    public PlayerLife PlayerLife => playerLife;

    [SerializeField] protected Image totalHealthBar;
    public Image TotalHeathBar => totalHealthBar;

    [SerializeField] protected Image currentHealthBar;
    public Image CurrentHeathBar => currentHealthBar;

    protected override void Start()
    {
        base.Start();
        this.LoadComponents();
        this.SetTotalHeathBar();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerHeath();
        this.LoadTotalHeathBar();
        this.LoadCurrentHeathBar();
    }

    protected override void Update()
    {
        base.Update();
        this.SetCurrentHeathBar();
    }

    protected virtual void LoadPlayerHeath()
    {
        if (this.playerLife != null) return;
        this.playerLife = GameObject.FindAnyObjectByType<PlayerLife>();
        Debug.LogWarning(transform.name + ": LoadPlayerHeath", gameObject);
    }

    protected virtual void LoadTotalHeathBar()
    {
        if(this.totalHealthBar!= null) return;
        this.totalHealthBar = transform.Find("HeathBarTotal").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadTotalHeathBar", gameObject);
    }

    protected virtual void LoadCurrentHeathBar()
    {
        if (this.currentHealthBar != null) return;
        this.currentHealthBar = transform.Find("HeathBarCurrent").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadCurrentHeathBar", gameObject);
    }

    protected virtual void SetTotalHeathBar()
    {
        this.totalHealthBar.fillAmount = playerLife.CurrentHeath / 10;
    }

    protected virtual void SetCurrentHeathBar()
    {
        this.currentHealthBar.fillAmount = playerLife.CurrentHeath / 10;
    }
}
