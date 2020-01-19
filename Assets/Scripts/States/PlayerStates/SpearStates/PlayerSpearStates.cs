using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpearStabForwardState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_stab");
        character.CanMove = false;
        character.CanFlip = false;
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanFlip = true;
    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (input == Vector2.zero)
            {
                character.CanMove = true;
                character.TransitionState(PlayerStates.SpearIdle);
            }
            else if (input.x != 0)
            {
                character.CanMove = true;
                character.TransitionState(PlayerStates.SpearRunningStart);
            }
        }
    }
}

public class PlayerSpearStabUpState : PlayerCharacterBaseState
{
    bool isFlaggedUnBlocking;
    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_stabup");
        character.CanMove = false;
        character.CanFlip = false;
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanFlip = true;
        character.CanMove = true;
        isFlaggedUnBlocking = false;
    }

    public override void Update(PlayerCharacter character)
    {
        CheckIfAnimationIsComplete(character);

        Vector2 input = character.dirInput;

        if (isAnimationComplete)
        {
            if (input.x != 0)
            {
                if (isFlaggedUnBlocking)
                {
                    character.TransitionState(PlayerStates.SpearIdle);
                }
                else
                {
                    character.TransitionState(PlayerStates.SpearRunningStart);
                }
            }

        }
    }

    public override void OnBlockButtonHold(PlayerCharacter character)
    {
        if (!isAnimationComplete)
            return;

        if (character.dirInput == Vector2.zero)
            character.TransitionState(PlayerStates.SpearBlocking);
        else
            character.TransitionState(PlayerStates.SpearBlockMove);
    }

    public override void OnBlockButtonUp(PlayerCharacter character)
    {
        isFlaggedUnBlocking = true;
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        isFlaggedUnBlocking = false;
    }
}


public class PlayerBlockingSpearStabForwardState : PlayerCharacterBaseState
{
    bool isFlaggedBlocking = false;
    bool isFlaggedAttacking = false;

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_blockstab");
        character.CanMove = false;
        character.CanFlip = false;

        isAnimationComplete = false;
        isFlaggedBlocking = false;
        isFlaggedAttacking = false;
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanFlip = true;
        character.CanMove = true;

        isFlaggedAttacking = false;
        isFlaggedBlocking = false;
    }

    public override void Update(PlayerCharacter character)
    {
        CheckIfAnimationIsComplete(character);

        Vector2 input = character.dirInput;

        if (isAnimationComplete)
        {
          
            if (isFlaggedAttacking)
            {
                if (input.y != 0)
                    character.TransitionState(PlayerStates.SpearStabUpBlock);
                else
                    character.TransitionState(PlayerStates.SpearStabForwardBlock);
            }

            if (input.x != 0)
            {
                if (isFlaggedBlocking)
                {
                    character.TransitionState(PlayerStates.SpearBlockMove);
                }
                else
                {
                    character.TransitionState(PlayerStates.SpearRunningStart);
                }
            }
            else
            {
                if (isFlaggedBlocking)
                {
                    character.TransitionState(PlayerStates.SpearBlock);
                }
                else
                {
                    character.TransitionState(PlayerStates.SpearIdle);
                }
            }
        }
    }

    public override void OnAttackButtonHold(PlayerCharacter character)
    {
        isFlaggedAttacking = true;
    }

    public override void OnAttackButtonUp(PlayerCharacter character)
    {
        isFlaggedAttacking = false;
    }

    public override void OnBlockButtonUp(PlayerCharacter character)
    {
        isFlaggedBlocking = false;
    }

    public override void OnBlockButtonHold(PlayerCharacter character)
    {
        isFlaggedBlocking = true;
    }

}

public class PlayerBlockingSpearStabUpState : PlayerCharacterBaseState
{
    bool isFlaggedUnBlocking;
    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_blockstabup");
        character.CanMove = false;
        character.CanFlip = false;
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanMove = true;
        character.CanFlip = true;
    }

    public override void Update(PlayerCharacter character)
    {
        CheckIfAnimationIsComplete(character);

        Vector2 input = character.dirInput;

        if (isAnimationComplete)
        {
            if (input.x != 0)
            {
                if (isFlaggedUnBlocking)
                {
                    character.TransitionState(PlayerStates.SpearIdle);
                }
                else
                {
                    character.TransitionState(PlayerStates.SpearRunningStart);
                }
            }

        }
    }

    public override void OnBlockButtonHold(PlayerCharacter character)
    {
        if (!isAnimationComplete)
            return;

        if (character.dirInput == Vector2.zero)
            character.TransitionState(PlayerStates.SpearBlocking);
        else
            character.TransitionState(PlayerStates.SpearBlockMove);
    }

    public override void OnBlockButtonUp(PlayerCharacter character)
    {
        isFlaggedUnBlocking = true;
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        isFlaggedUnBlocking = false;
    }
}

public class PlayerSpearThrowState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_throw");
        character.CanMove = false;
        character.CanFlip = false;
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanMove = true;
        character.CanFlip = true;
    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (character.IsGrounded)
            {
                if (input == Vector2.zero)
                {
                    if (Input.GetKey(KeyCode.L))
                    {
                        character.TransitionState(PlayerStates.SpearBlock);
                    }
                    else
                    {
                        character.TransitionState(PlayerStates.SpearIdle);
                    }
                }
                else if (input.x != 0)
                {
                    if (Input.GetKey(KeyCode.L))
                    {
                        character.TransitionState(PlayerStates.SpearBlockMove);
                    }
                    else
                    {
                        character.TransitionState(PlayerStates.SpearRunningStart);
                    }
                }
            }
            else
            {
                character.TransitionState(PlayerStates.SpearJumpDown);
            }

        }
    }
}
