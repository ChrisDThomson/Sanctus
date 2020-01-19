using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : Controller2D
{
    public PlayerCollisionInfo Collisions
    {
        get { return (PlayerCollisionInfo)collisions; }
    }

    public override void Start()
    {
        collisions = new PlayerCollisionInfo();
        base.Start();     
    }

    protected override void HorizontalCollisions(ref Vector2 moveAmount)
    {
        base.HorizontalCollisions(ref moveAmount);

        CheckForLedge(ref moveAmount);
    }

    /// <summary>
    /// The controller shoots a ray from the top of it's collider and another ray toward the ground slightly to detect a ledge
    /// This creates a hook of some sort for ledge detection
    /// 
    /// Our collider
    /// 
    ///   __  ___ ray
    ///  |  |    |  downward ray
    ///  |__|    - 
    ///  
    /// 
    /// THIS DOES NOT WORK WITH MOVING PLATFORMS -  if the need arises will need to be refactored...
    /// </summary>
    /// <param name="moveAmount"></param>
    public void CheckForLedge(ref Vector2 moveAmount)
    {     
        float directionX = collisions.faceDir;

        //an arbitrary amount of distance for both rays to travel,
        //chose the size of the bounds x for now
        float rayLength = collider.bounds.extents.x;

        //chose a ray based on our face dir
        Vector2 wallCheckRay = (directionX == -1) ? raycastOrigins.topLeft : raycastOrigins.topRight;

        //calculate the origin of our ray shot downwards (end of the first ray)
        Vector2 dst = Vector2.right * rayLength * directionX;
        Vector2 ledgeCheckRay = wallCheckRay + dst;

        //Raycast to see if we are hitting a ledge
        RaycastHit2D checkWallHit = Physics2D.Raycast(wallCheckRay, Vector2.right * directionX, rayLength, collisionMask);
        RaycastHit2D checkLedgeHit = Physics2D.Raycast(ledgeCheckRay, Vector2.down, rayLength, collisionMask);

        //For easy visualization at run time
        Debug.DrawRay(ledgeCheckRay, Vector2.down * rayLength, Color.blue);
        Debug.DrawRay(wallCheckRay, Vector2.right * rayLength * directionX, Color.blue);

        //if the checkLedge hit has a collider2D(a platform)
        bool isThroughPlatform = false;
        if (checkLedgeHit.collider)
        {
            isThroughPlatform = checkLedgeHit.collider.tag == "Through";
        }

        //if we didnt hit a wall but we did hit some ground 
        //check if we are not falling through or trying to grab a platform we can jump through
        if (!checkWallHit && checkLedgeHit && !isThroughPlatform)
        {
            //stop moving us - so that gravity doesnt keep pushing us downward
            moveAmount = Vector2.zero;

            //calculate where we hit the ledge and adjust on the y for skin(this is to prevent going through the floor later)
            //we need the ledge point, so we can teleport the collider, on top of the ledge when the animation is done playing
            //during the animation the collider is still hugging the side of the wall
            Vector2 ledgePoint = checkLedgeHit.point;
            ledgePoint.y += (skinWidth * 2);

            //update our collision info
            ((PlayerCollisionInfo)collisions).ledgeHitPoint = ledgePoint;
            ((PlayerCollisionInfo)collisions).grabLedge = true;
        }
    }

    [System.Serializable]
    public class PlayerCollisionInfo : CollisionInfo
    {
        public bool grabLedge;
        public Vector2 ledgeHitPoint;

        public override void Reset()
        {
            if (!grabLedge)
            {
                ledgeHitPoint = Vector2.zero;
            }

            grabLedge = false;

            base.Reset();
        }
    }
}
