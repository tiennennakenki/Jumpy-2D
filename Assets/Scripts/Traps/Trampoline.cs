using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : TrapsManager
{
    [Header("Trampoline")]
    [SerializeField] protected float bounce = 30f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.animator.SetTrigger("jump");

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * this.bounce, ForceMode2D.Impulse);
        }
    }
}

