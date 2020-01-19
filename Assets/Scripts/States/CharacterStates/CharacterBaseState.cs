

using UnityEngine;

public abstract class BaseState<T>
{
    public abstract void EnterState(T character);
    public abstract void Update(T character);

    public abstract void OnTransition(T character);
}


public abstract class CharacterBaseState<T> : BaseState<T>
{

}

public abstract class PlayerCharacterBaseState : CharacterBaseState<PlayerCharacter>
{
    public virtual void OnJumpButtonDown(PlayerCharacter character) { }
    public virtual void OnJumpButtonUp(PlayerCharacter character) { }
    public virtual void OnJumpButtonHold(PlayerCharacter character) { }

    public virtual void OnAttackButtonDown(PlayerCharacter character) { }
    public virtual void OnAttackButtonUp(PlayerCharacter character) { }
    public virtual void OnAttackButtonHold(PlayerCharacter character) { }

    public virtual void OnBlockButtonDown(PlayerCharacter character) { }
    public virtual void OnBlockButtonUp(PlayerCharacter character) { }
    public virtual void OnBlockButtonHold(PlayerCharacter character) { }

    public virtual void OnThrowButtonDown(PlayerCharacter character) { }
    public virtual void OnThrowButtonUp(PlayerCharacter character) { }
    public virtual void OnThrowButtonHold(PlayerCharacter character) { }


    protected bool isAnimationComplete;
    public void CheckIfAnimationIsComplete(PlayerCharacter character, float timeToCheckAgainst = 1.0f)
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > (1))
        {
            isAnimationComplete = true;
        }
    }
}

public class PlayerCharacterBlockBaseState : PlayerCharacterBaseState
{
    public override void EnterState(PlayerCharacter character)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTransition(PlayerCharacter character)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(PlayerCharacter character)
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttackButtonDown(PlayerCharacter character)
    {
        if (character.dirInput.y > 0)
            character.TransitionState(PlayerStates.SpearStabUpBlock);
        else
            character.TransitionState(PlayerStates.SpearStabForwardBlock);
    }

    public override void OnJumpButtonDown(PlayerCharacter character)
    {
        character.TransitionState(PlayerStates.SpearJumpStart);
    }
}

