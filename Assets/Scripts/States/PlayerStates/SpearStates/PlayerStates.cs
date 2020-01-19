using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpearIdleState : PlayerCharacterBaseState
{
    public int faceDir;

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_stand");
        character.dirInput = Vector2.zero;
        faceDir = character.controller.collisions.faceDir;
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {

        Vector2 input = character.dirInput;

        //if we are falling
        if (!character.IsGrounded)
        {
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        //if we moving and we are facing our current direction
        if (input.x != 0.0f && Mathf.Sign(input.x) == faceDir)
        {
            character.TransitionState(PlayerStates.SpearRunningStart);
        }
        //if we moving and we facing the opposite way - pivot first
        else if (input.x != 0.0f && Mathf.Sign(input.x) != faceDir)
        {
            character.TransitionState(PlayerStates.SpearPivot);
        }
    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (input.y > 0)
            character.TransitionState(PlayerStates.SpearStabUp);
        else
            character.TransitionState(PlayerStates.SpearStabForward);
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearJumpStart);
    }

    public override void OnThrowButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearThrow);
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearBlock);
    }
}

public class PlayerSpearPivotState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_pivot");
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (!character.IsGrounded)
        {
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (character.velocity.x == 0 && input.x == 0)
                character.TransitionState(PlayerStates.SpearIdle);
            else
                character.TransitionState(PlayerStates.SpearRunningStart);


        }
    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (input.y > 0)
            character.TransitionState(PlayerStates.SpearStabUp);
        else
            character.TransitionState(PlayerStates.SpearStabForward);
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearJumpStart);
    }

    public override void OnThrowButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearThrow);
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearBlock);
    }
}

public class PlayerSpearRunningState : PlayerCharacterBaseState
{
    public int faceDir;
    public override void EnterState(PlayerCharacter character)
    {
        character.Animator.Play("G_H_run");
        faceDir = character.controller.Collisions.faceDir;
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (!character.IsGrounded)
        {
            character.TransitionState(PlayerStates.SpearJumpDown);

        }

        if (input.x == 0.0f)
            character.TransitionState(PlayerStates.SpearRunningStop);

        if (input.x != 0.0f && Mathf.Sign(input.x) != faceDir)
        {
            character.TransitionState(PlayerStates.SpearPivot);
        }

    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (input.y > 0)
            character.TransitionState(PlayerStates.SpearStabUp);
        else
            character.TransitionState(PlayerStates.SpearStabForward);
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearJumpStart);
    }

    public override void OnThrowButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearThrow);
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearBlock);
    }
}

public class PlayerSpearRunningStartState : PlayerCharacterBaseState
{


    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_runstart");
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (!character.IsGrounded)
        {
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            character.TransitionState(PlayerStates.SpearRunning);
        }

        if (input == Vector2.zero)
            character.TransitionState(PlayerStates.SpearIdle);
    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (input.y > 0)
            character.TransitionState(PlayerStates.SpearStabUp);
        else
            character.TransitionState(PlayerStates.SpearStabForward);
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearJumpStart);
    }

    public override void OnThrowButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearThrow);
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearBlock);
    }
}

public class PlayerSpearRunningStopState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_runstop");
        //character.velocity.x = 0.01f * character.controller.Collisions.faceDir;
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (!character.IsGrounded)
        {
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            character.TransitionState(PlayerStates.SpearIdle);
        }
    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (input.y > 0)
            character.TransitionState(PlayerStates.SpearStabUp);
        else
            character.TransitionState(PlayerStates.SpearStabForward);
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearJumpStart);
    }

    public override void OnThrowButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearThrow);
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearBlock);
    }
}

public class PlayerSpearHurtState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_injuredG");
        character.CanMove = false;
        character.velocity = Vector2.zero;
        character.velocity.x = character.controller.collisions.faceDir * 0.01f;
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanMove = true;
    }

    public override void Update(PlayerCharacter character)
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (character.controller.collisions.below)
            {
                character.TransitionState(PlayerStates.SpearIdle);
            }
        }

    }
}

public class PlayerSpearHurtAirState : PlayerCharacterBaseState
{
    float oldGravity = 0;

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_injuredA");
        character.CanMove = false;
        character.velocity = Vector2.zero;
        character.velocity.x = character.controller.collisions.faceDir * 0.01f;

        if (oldGravity == 0)
            oldGravity = character.gravity;
        else
            character.gravity = oldGravity;

        character.gravity = 0;
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanMove = true;
        character.gravity = oldGravity;
    }

    public override void Update(PlayerCharacter character)
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (character.controller.collisions.below)
            {
                character.TransitionState(PlayerStates.SpearIdle);
            }
            else
            {
                character.TransitionState(PlayerStates.SpearJumpDown);
            }
        }

    }
}
