using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : SaiMonoBehaviour
{
    public static event Action CreateDestroyed = delegate { };

    [SerializeField] protected Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("hit");
            //Invoke("DestroyedCrate", .20f);
        }
    }

    //protected virtual void DestroyedCrate()
    //{
    //    animator.gameObject.SetActive(false);
    //    Destroy(transform.gameObject);
    //}
}
