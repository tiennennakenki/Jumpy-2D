using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : TrapsManager
{
    [Header("Fire")]
    protected bool isTriggerOn = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.animator.SetTrigger("isHit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.animator.SetTrigger("isOn");
            this.isTriggerOn = true;
            Invoke("SetTagFire", .75f);

        }
    }

    protected virtual void SetTagFire()
    {
        this.gameObject.tag = "Fire";
        Invoke("UnsetTagFire", 1f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);

        // Kiểm tra xem raycast có hit vào player không
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // Nếu hit vào player, gọi hàm gây damage
            PlayerLife playerHealth = hit.collider.GetComponent<PlayerLife>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    protected virtual void UnsetTagFire()
    {
        this.gameObject.tag = "Untagged";
        this.isTriggerOn = false;
    }


}
