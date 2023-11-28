using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SaiMonoBehaviour
{
    [SerializeField] public Transform player;

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
        GameObject playerManager = GameObject.Find("PlayerManager");

        if (playerManager != null)
        {
            // Get the first child's transform
            Transform firstChild = playerManager.transform.GetChild(0);

            if (firstChild != null)
            {
                this.player = firstChild;
                Debug.Log(transform.name + ": LoadPlayer - Player loaded", gameObject);
            }
            else
            {
                Debug.LogWarning("PlayerManager has no children.", playerManager);
            }
        }
        else
        {
            Debug.LogError("PlayerManager not found in the scene.");
        }
    }

    protected virtual Vector3 FollowPlayer()
    {
        return transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
