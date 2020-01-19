using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateController : StateController<PlayerStates>
{
    PlayerCharacter character;
    PlayerCharacterBaseState currentState = null;

    public override Animator Animator { get => character.Animator; }
    public PlayerCharacterBaseState CurrentState { get => currentState; }
    public PlayerInputController inputController;

    public bool IsBlocking
    {
        get
        {
            if (currentState == spearBlockState || currentState == spearBlockRunState || currentState == spearBlocking)
                return true;
            else
                return false;
        }
    }

    public PlayerStateController(PlayerCharacter playerCharacter)
    {
        character = playerCharacter;
    }

    // Update is called once per frame
    public override void Update()
    {
        currentState.Update(character);
    }

    public override void TransitionState(PlayerStates state)
    {
        if (currentState != null)
            currentState.OnTransition(character);

        currentState = GetPlayerState(state);
        currentStateName = currentState.ToString();

        currentState.EnterState(character);
    }

    public void OnActionInput(InputAction action, InputType type)
    {
        switch (action)
        {
            case InputAction.Action1:
                HandleAction1(type);
                break;
            case InputAction.Action2:
                HandleAction2(type);
                break;
            case InputAction.Action3:
                HandleAction3(type);
                break;
            case InputAction.Action4:
                HandleAction4(type);
                break;

            default:
                Debug.Log("Invalid Input Type!");
                break;
        }

    }

    //Attack
    void HandleAction1(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Pressed:
                currentState.OnAttackButtonDown(character);
                break;
            case InputType.Released:
                currentState.OnAttackButtonUp(character);
                break;
            case InputType.Held:
                currentState.OnAttackButtonHold(character);
                break;
        }
    }

    //Block
    void HandleAction2(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Pressed:
                currentState.OnBlockButtonDown(character);
                break;
            case InputType.Released:
                currentState.OnBlockButtonUp(character);
                break;
            case InputType.Held:
                currentState.OnBlockButtonHold(character);
                break;
        }
    }

    //Throw
    void HandleAction3(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Pressed:
                currentState.OnThrowButtonDown(character);
                break;
            case InputType.Released:
                currentState.OnThrowButtonUp(character);
                break;
            case InputType.Held:
                currentState.OnThrowButtonHold(character);
                break;
        }
    }

    //Jump
    void HandleAction4(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Pressed:
                Debug.Log("Jump");
                currentState.OnJumpButtonDown(character);
                break;
            case InputType.Released:
                currentState.OnJumpButtonUp(character);
                break;
            case InputType.Held:
                currentState.OnJumpButtonHold(character);
                break;
        }
    }

    public PlayerCharacterBaseState GetPlayerState(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.SpearIdle:
                return spearIdleState;
            case PlayerStates.SpearPivot:
                return spearPivotState;
            case PlayerStates.SpearRunningStart:
                return spearRunningStartState;
            case PlayerStates.SpearRunning:
                return spearRunningState;
            case PlayerStates.SpearRunningStop:
                return spearRunningStopState;
            case PlayerStates.SpearJumpStart:
                return spearJumpStartState;
            case PlayerStates.SpearJumpUp:
                return spearJumpUpState;
            case PlayerStates.SpearJumpPeak:
                return spearJumpPeakState;
            case PlayerStates.SpearJumpDown:
                return spearJumpDownState;
            case PlayerStates.SpearJumpLand:
                return spearJumpLandState;
            case PlayerStates.SpearStabForward:
                return spearStabForwardState;
            case PlayerStates.SpearStabUp:
                return spearStabUpState;
            case PlayerStates.SpearStabForwardBlock:
                return spearBlockingStabForwardState;
            case PlayerStates.SpearStabUpBlock:
                return spearBlockingStabUpState;
            case PlayerStates.SpearLedgeGrab:
                return spearLedgeGrabState;
            case PlayerStates.SpearBlock:
                return spearBlockState;
            case PlayerStates.SpearBlockedHit:
                return new PlayerSpearBlockedHitState();
            case PlayerStates.SpearBlockMove:
                return spearBlockRunState;
            case PlayerStates.SpearUnBlock:
                return spearUnblockState;
            case PlayerStates.SpearHurt:
                return spearHurtState;
            case PlayerStates.SpearHurtAir:
                return spearHurtAirState;
            case PlayerStates.SpearThrow:
                return spearThrowState;
            case PlayerStates.SpearBlocking:
                return spearBlocking;
            case PlayerStates.SpearDeath:

            case PlayerStates.Idle:

            case PlayerStates.Pivot:

            case PlayerStates.RunningStart:

            case PlayerStates.Running:

            case PlayerStates.RunningStop:

            case PlayerStates.JumpStart:

            case PlayerStates.JumpUp:

            case PlayerStates.JumpPeak:

            case PlayerStates.JumpDown:

            case PlayerStates.JumpLand:

            case PlayerStates.StabForward:

            case PlayerStates.StabUp:

            case PlayerStates.LedgeGrab:

            case PlayerStates.Block:

            case PlayerStates.BlockedHit:

            case PlayerStates.BlockMove:

            case PlayerStates.UnBlock:

            case PlayerStates.Hurt:

            case PlayerStates.HurtAir:


            default:
                Debug.Log(state);
                return spearIdleState;
        }
    }

    public readonly PlayerCharacterBaseState spearIdleState = new PlayerSpearIdleState();
    public readonly PlayerCharacterBaseState spearRunningState = new PlayerSpearRunningState();
    public readonly PlayerCharacterBaseState spearRunningStartState = new PlayerSpearRunningStartState();
    public readonly PlayerCharacterBaseState spearRunningStopState = new PlayerSpearRunningStopState();

    public readonly PlayerCharacterBaseState spearPivotState = new PlayerSpearPivotState();

    public readonly PlayerCharacterBaseState spearJumpStartState = new PlayerSpearJumpStartState();
    public readonly PlayerCharacterBaseState spearJumpUpState = new PlayerSpearJumpUpState();
    public readonly PlayerCharacterBaseState spearJumpPeakState = new PlayerSpearJumpPeakState();
    public readonly PlayerCharacterBaseState spearJumpDownState = new PlayerSpearJumpDownState();
    public readonly PlayerCharacterBaseState spearJumpLandState = new PlayerSpearJumpLandState();

    public readonly PlayerCharacterBaseState spearStabForwardState = new PlayerSpearStabForwardState();
    public readonly PlayerCharacterBaseState spearStabUpState = new PlayerSpearStabUpState();

    public readonly PlayerCharacterBaseState spearBlockingStabForwardState = new PlayerBlockingSpearStabForwardState();
    public readonly PlayerCharacterBaseState spearBlockingStabUpState = new PlayerBlockingSpearStabUpState();

    public readonly PlayerCharacterBaseState spearLedgeGrabState = new PlayerSpearLedgeGrabState();

    public readonly PlayerCharacterBaseState spearBlockState = new PlayerSpearBlockState();
    public readonly PlayerCharacterBaseState spearBlocking = new PlayerSpearBlockingState();
    public readonly PlayerCharacterBaseState spearUnblockState = new PlayerSpearUnBlockState();
    public readonly PlayerCharacterBaseState spearBlockRunState = new PlayerSpearBlockMoveState();

    public readonly PlayerCharacterBaseState spearHurtState = new PlayerSpearHurtState();
    public readonly PlayerCharacterBaseState spearHurtAirState = new PlayerSpearHurtAirState();

    public readonly PlayerCharacterBaseState spearThrowState = new PlayerSpearThrowState();
}
