using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpearBlockState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_block");
        character.dirInput = Vector2.zero;
        character.CanMove = false;
        character.CanFlip = false;
        character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Blocking);
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanMove = true;
    }

    public override void Update(PlayerCharacter character)
    {
        CheckIfAnimationIsComplete(character);

        if (isAnimationComplete)
        {
            if (!character.blockBox.gameObject.activeSelf)
                character.blockBox.ToggleActive(true);

            character.TransitionState(PlayerStates.SpearBlocking);
        }

        Vector2 input = character.dirInput;

        if (!character.IsGrounded)
        {
            character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
            character.blockBox.gameObject.SetActive(false);
            character.CanFlip = true;
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        if (input.x != 0)
        {
            character.TransitionState(PlayerStates.SpearBlockMove);
        }
    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        if (character.dirInput.y > 0)
        {
            character.TransitionState(PlayerStates.SpearStabUpBlock);
        }
        else
        {
            character.TransitionState(PlayerStates.SpearStabForwardBlock);
        }
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.CanFlip = true;
        character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
        character.blockBox.gameObject.SetActive(false);
        character.TransitionState(PlayerStates.SpearJumpStart);
    }

    public override void OnBlockButtonUp(PlayerCharacter character)
    {
        character.CanFlip = true;
        character.TransitionState(PlayerStates.SpearUnBlock);
    }
}

public class PlayerSpearBlockMoveState : PlayerCharacterBaseState
{
    float oldMoveSpeed = 0;

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_blockmove");

        character.CanFlip = false;

        //if this is the first time entering this state then
        if (oldMoveSpeed == 0)
            oldMoveSpeed = character.moveSpeed;
        else
            character.moveSpeed = oldMoveSpeed;

        character.moveSpeed /= 3;

        character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Blocking);
        character.blockBox.ToggleActive(true);
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.moveSpeed = oldMoveSpeed;
    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (!character.IsGrounded)
        {
            character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
            character.blockBox.ToggleActive(false);
            character.CanFlip = true;
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        if (Mathf.Abs(character.velocity.x) <= 0.5f && input.x == 0)
        {
            character.TransitionState(PlayerStates.SpearBlocking);
        }
    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        if (character.dirInput.y > 0)
        {
            character.TransitionState(PlayerStates.SpearStabUpBlock);
        }
        else
        {
            character.TransitionState(PlayerStates.SpearStabForwardBlock);
        }
    }

    public override void OnBlockButtonUp(PlayerCharacter character)
    {
        character.CanFlip = true;
        character.TransitionState(PlayerStates.SpearUnBlock);
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.CanFlip = true;
        character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
        character.blockBox.ToggleActive(false);
        character.TransitionState(PlayerStates.SpearJumpStart);
    }
}


public class PlayerSpearUnBlockState : PlayerCharacterBaseState
{
    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_unblock");
        character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
        character.blockBox.gameObject.SetActive(false);
        isAnimationComplete = false;
    }

    public override void OnTransition(PlayerCharacter character)
    {

    }

    public override void Update(PlayerCharacter character)
    {
        CheckIfAnimationIsComplete(character);

        Vector2 input = character.dirInput;

        if (!character.IsGrounded)
        {
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        if (isAnimationComplete)
        {
            if (input.x != 0)
            {
                character.TransitionState(PlayerStates.SpearRunningStart);
            }

            character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
            character.blockBox.gameObject.SetActive(false);
            character.TransitionState(PlayerStates.SpearIdle);
        }
    }

    public override void OnBlockButtonUp(PlayerCharacter character)
    {
        if (isAnimationComplete)
            character.TransitionState(PlayerStates.SpearIdle);
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        if (!isAnimationComplete)
            character.TransitionState(PlayerStates.SpearBlock);
    }
}

public class PlayerSpearBlockedHitState : PlayerCharacterBaseState
{
    float blockTime = 0;
    bool isFlaggedToUnblock = false;

    //public PlayerBlockedHitState(float damageBlocked)
    //{
    //    //Every 10 bits of damage add more time stuck in block
    //    int time = (int)(damageBlocked / 10);
    //    blockTime = Mathf.Clamp(time, 0,5);
    //}

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_blockblock");
        character.dirInput = Vector2.zero;
        character.CanMove = false;
        character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Blocking);
    }

    public override void OnTransition(PlayerCharacter character)
    {
        isFlaggedToUnblock = false;
        character.CanMove = true;
    }

    public override void Update(PlayerCharacter character)
    {
        CheckIfAnimationIsComplete(character, 1 + blockTime);

        if (isAnimationComplete)
        {
            Vector2 input = character.dirInput;

            if (isFlaggedToUnblock)
            {
                character.TransitionState(PlayerStates.SpearUnBlock);
            }

            if (!character.IsGrounded)
            {
                character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
                character.blockBox.gameObject.SetActive(false);
                character.TransitionState(PlayerStates.SpearJumpDown);
            }

            if (input.x != 0)
            {
                character.TransitionState(PlayerStates.SpearBlockMove);
            }
        }
    }

    public override void OnBlockButtonUp(PlayerCharacter character)
    {
        if (isAnimationComplete)
            character.TransitionState(PlayerStates.SpearIdle);
        else
            isFlaggedToUnblock = true;
    }

    public override void OnBlockButtonDown(PlayerCharacter character)
    {
        if (isAnimationComplete)
            character.TransitionState(PlayerStates.SpearBlock);
        else
            isFlaggedToUnblock = false;
    }

    public override void OnBlockButtonHold(PlayerCharacter character)
    {
        if (isAnimationComplete)
            character.TransitionState(PlayerStates.SpearBlock);
    }
}

public class PlayerSpearBlockingState : PlayerCharacterBaseState
{

    public override void EnterState(PlayerCharacter character)
    {
        //update animation
        character.Animator.Play("G_H_blocking");
        character.dirInput = Vector2.zero;
        character.CanMove = false;
        character.CanFlip = false;
        character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Blocking);

        if (!character.blockBox.gameObject.activeSelf)
            character.blockBox.ToggleActive(true);
    }

    public override void OnTransition(PlayerCharacter character)
    {
        character.CanMove = true;
    }

    public override void Update(PlayerCharacter character)
    {
        Vector2 input = character.dirInput;

        if (!character.IsGrounded)
        {
            character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
            character.blockBox.gameObject.SetActive(false);
            character.CanFlip = true;
            character.TransitionState(PlayerStates.SpearJumpDown);
        }

        if (input.x != 0)
        {
            character.TransitionState(PlayerStates.SpearBlockMove);
        }
    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        if (character.dirInput.y > 0)
        {
            character.TransitionState(PlayerStates.SpearStabUpBlock);
        }
        else
        {
            character.TransitionState(PlayerStates.SpearStabForwardBlock);
        }
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.CanFlip = true;
        character.hurtBoxController.ChangeHurtBox(HurtBoxController.HurtBoxType.Normal);
        character.blockBox.gameObject.SetActive(false);
        character.TransitionState(PlayerStates.SpearJumpStart);
    }

    public override void OnBlockButtonUp(PlayerCharacter character)
    {
        character.CanFlip = true;
        character.TransitionState(PlayerStates.SpearUnBlock);
    }
}