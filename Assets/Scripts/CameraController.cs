using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SaiMonoBehaviour
{
    [SerializeField] protected Transform player;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }

    protected override void Update()
    {
        this.FollowPlayer();
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.Find("Player").transform;
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    protected virtual Vector3 FollowPlayer()
    {
        return transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
