using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemsManager : SaiMonoBehaviour
{
    public virtual void DestroyShatteredCrate()
    {
        //For override
    }

    public virtual void DestroyedObj()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject); // Use gameObject instead of transform.gameObject
        }
    }
}
