using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : SaiMonoBehaviour
{
    [SerializeField] protected GameObject[] wayPoints;
    protected int currentIndex = 0;

    [SerializeField] protected float speed = 2f;
    public float Speed => speed;

    protected override void Update()
    {
        base.Update();
        this.Moving();
    }

    protected virtual void Moving()
    {
        if (Vector2.Distance(wayPoints[this.currentIndex].transform.position, transform.position) < .1f)
        {
            this.currentIndex++;
            if(this.currentIndex >= wayPoints.Length)
            {
                this.currentIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[this.currentIndex].transform.position, this.speed * Time.deltaTime);
    }
}
