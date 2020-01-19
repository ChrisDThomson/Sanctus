using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearStateController : StateController<SpearStates>
{
    Spear spear;
    public override Animator Animator => spear.animator;

    protected CharacterBaseState<Spear> currentState = null;


    public SpearStateController(Spear _spear)
    {
        spear = _spear;
    }

    public readonly CharacterBaseState<Spear> thrownState = new SpearThrownState();
    public readonly CharacterBaseState<Spear> wallHitState = new SpearWallHitState();
    public readonly CharacterBaseState<Spear> bounceState = new SpearBounceState();

    // Update is called once per frame
    public override void Update()
    {
        currentState.Update(spear);
    }

    public override void TransitionState(SpearStates state)
    {
        if (currentState != null)
            currentState.OnTransition(spear);

        currentState = GetSpearState(state);
        currentStateName = currentState.ToString();
        currentState.EnterState(spear);
    }

    CharacterBaseState<Spear> GetSpearState(SpearStates state)
    {
        switch (state)
        {
            case SpearStates.spearThrown:
                return thrownState;
            case SpearStates.spearWallHit:
                return wallHitState;
            case SpearStates.spearBounce:
                return bounceState;
                break;
            default:
                return thrownState;
        }
    }
}