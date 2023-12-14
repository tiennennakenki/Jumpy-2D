using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : EnemyManager
{
    [SerializeField] protected int countHit = 3;
    public int CountHit => countHit;

    [SerializeField] protected float bounce = 30f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.isInvincible)
        {
            this.isInvincible = true;
            this.countHit--;
            this.animator.SetTrigger("isHurt");
            this.HurtSoundEffect.Play();

            Invoke("DisableInvincibility", 2f);

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * this.bounce, ForceMode2D.Impulse);
            if (this.countHit == 0)
            {
                this.animator.SetTrigger("isDead");
                Invoke("DeadSoundEffectPlay", 1.1f);
                Invoke("OnDeathAnimationComplete", 1.5f);
            }
        }
    }
}
