using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : TrapsManager
{
    //[Header("Spike Head")]

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;

            if (Mathf.Abs(normal.y) > Mathf.Abs(normal.x))
            {
                if (normal.y > 0)
                {
                    //this.state = SpikeHeadState.bottomHit;
                    this.animator.SetTrigger("topHit");
                }
                else
                {
                    //this.state = SpikeHeadState.topHit;
                    this.animator.SetTrigger("bottomHit");
                }
            }
            else
            {
                // Va chạm phía trái hoặc phải
                if (normal.x < 0)
                {
                    //this.state = SpikeHeadState.rightHit;
                    this.animator.SetTrigger("leftHit");
                }
                else
                {
                    this.animator.SetTrigger("rightHit");
                }
            }
        }
        //this.animator.SetInteger("state", (int)state);
    }
}
