using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpearJumpStartState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_jumpstart");


        character.velocity.y = character.maxJumpVelocity;

    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            character.TransitionState(PlayerStates.SpearJumpUp);
        }


    }
}

public class PlayerSpearJumpUpState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_jumpup");
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {

        if (character.controller.Collisions.grabLedge)
        {
            character.TransitionState(PlayerStates.SpearLedgeGrab);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            character.TransitionState(PlayerStates.SpearThrow);
        }

        if (character.velocity.y <= 0.0f)
        {
            character.TransitionState(PlayerStates.SpearJumpPeak);
        }
    }
}

public class PlayerSpearJumpPeakState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_jumppeak");
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {

        if (character.controller.Collisions.grabLedge)
        {
            character.TransitionState(PlayerStates.SpearLedgeGrab);
        }

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            character.TransitionState(PlayerStates.SpearThrow);
        }
    }
}

public class PlayerSpearJumpDownState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_jumpdown");
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {

        if (character.controller.Collisions.grabLedge)
        {
            character.TransitionState(PlayerStates.SpearLedgeGrab);
        }

        if (character.IsGrounded)
        {
            character.TransitionState(PlayerStates.SpearJumpLand);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            character.TransitionState(PlayerStates.SpearThrow);
        }
    }
}

public class PlayerSpearJumpLandState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_jumpland");
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (input.x == 0)
                character.TransitionState(PlayerStates.SpearIdle);
            else
                character.TransitionState(PlayerStates.SpearRunningStart);
        }        
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearJumpStart);
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearBlock);
    }

    public override void OnThrowButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearThrow);
    }
}

public class PlayerSpearLedgeGrabState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_ledgeclimb");
        character.CanMove = false;
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            character.CanMove = true;
            character.transform.position = character.controller.Collisions.ledgeHitPoint;

            if (Input.GetKey(KeyCode.L))
            {
                if (input == Vector2.zero)
                    character.TransitionState(PlayerStates.SpearBlock);
                else
                    character.TransitionState(PlayerStates.SpearBlockMove);
            }
            else
            {
                if (input == Vector2.zero)
                    character.TransitionState(PlayerStates.SpearIdle);
                else
                    character.TransitionState(PlayerStates.SpearRunningStart);
            }
        }
    }
}

