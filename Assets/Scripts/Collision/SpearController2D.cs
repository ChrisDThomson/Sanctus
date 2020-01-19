using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController2D : Controller2D
{
    public SpearCollisionInfo Collisions
    {
        get { return (SpearCollisionInfo)collisions; }
    }

    public override void Start()
    {
        base.Start();

        collisions = new SpearCollisionInfo();
    }

    protected override void HorizontalCollisions(ref Vector2 moveAmount)
    {
        base.HorizontalCollisions(ref moveAmount);

        float directionX = Collisions.faceDir;
        Vector2 center = collider.bounds.center;

        if (directionX == -1)
            center.x = collider.bounds.min.x;
        else
            center.x = collider.bounds.max.x;

        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

        RaycastHit2D hit = Physics2D.Raycast(center, Vector2.right * directionX, rayLength, collisionMask);
        Debug.DrawRay(center, Vector2.right * directionX * rayLength * transform.localScale.x, Color.yellow);   

        if (hit)
        {

            Debug.DrawRay(hit.point, hit.normal, Color.magenta);
        }
    }

    [System.Serializable]
    public class SpearCollisionInfo : CollisionInfo
    {
        public bool shouldBounce;
        public Vector2 hitNormalDir;

        public override void Reset()
        {
            base.Reset();
        }
    }

}
