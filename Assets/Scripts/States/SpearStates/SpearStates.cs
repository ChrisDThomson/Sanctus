using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrownState : CharacterBaseState<Spear>
{
    public override void EnterState(Spear spear)
    {
        spear.animator.Play("thrownSpear");
    }

    public override void OnTransition(Spear spear)
    {
    }

    public override void Update(Spear spear)
    {
        if(spear.controller.Collisions.shouldBounce && spear.controller.Collisions.climbingSlope)
        {
            spear.TransitionState(SpearStates.spearBounce);
        }
        else if(spear.DidSpearHeadCollide)
        {
            spear.TransitionState(SpearStates.spearWallHit);
        }
    }
}

public class SpearWallHitState : CharacterBaseState<Spear>
{
    public override void EnterState(Spear spear)
    {
        spear.animator.Play("wallStuckSpear");
        spear.CanMove = false;
        spear.velocity.x = 0;
        spear.velocity.y = 0;
    }

    public override void OnTransition(Spear spear)
    {
    }

    public override void Update(Spear spear)
    {

    }
}

public class SpearBounceState : CharacterBaseState<Spear>
{
    public override void EnterState(Spear spear)
    {
        spear.animator.Play("spinning");
        spear.velocity = spear.controller.Collisions.hitNormalDir;
    }

    public override void OnTransition(Spear spear)
    {
    }

    public override void Update(Spear spear)
    {

    }
}
