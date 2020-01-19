using Sanctus.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerCharacter : Character<PlayerController2D>, IBlockable, IParryable
{
    public HurtBoxController hurtBoxController;
    public BlockBox blockBox;

    PlayerInputController playerInputController;

    [SerializeField]
    [ReadOnly]
    string currentStateName;

    [ReadOnly]
    public Vector3 velocity;

    public float moveSpeed = 2;

    public float gravity = -10.0f;

    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;

    float velocityXSmoothing;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;

    public float timeToJumpApex = .4f;

    public bool CanMove = true;
    public bool CanFlip = true;

    public PlayerStateController playerStateController;

    [HideInInspector]
    public float maxJumpVelocity;
    [HideInInspector]
    public float minJumpVelocity;

    [SerializeField]
    Animator animator;

    public Animator Animator { get { return animator; } }

    /// <summary>
    /// This was setup to solve a bug where for one frame, the controller2D thought it was in the air
    /// This was the easiest solution : storing our last grounded state
    /// The player wont realise it's in the air until the 2nd frame of air time
    /// </summary>
    public bool IsGrounded
    {
        get
        {
            bool below = controller.collisions.below;
            bool lastFrameBelow = controller.collisions.belowOld;

            //if we are touching ground
            if (below)
                return true;

            // if we are not currently touching ground but we did last frame
            if (!below && lastFrameBelow)
                return true;

            // if we are currently touching ground but last frame we were airborne
            if (below && !lastFrameBelow)
                return true;

            //we are airborne
            return false;
        }
    }

    protected override void Init()
    {
        base.Init();

        //get a reference to our input controller
        playerInputController = new PlayerInputController();
        playerInputController.playerCharacter = this;

        //Create our state controller to manage all our states
        playerStateController = new PlayerStateController(this);
        //init player to idle
        TransitionState(PlayerStates.SpearIdle);

        //get our hurtboxes
        hurtBoxController = GetComponentInChildren<HurtBoxController>();

        //get our blocking box and set to unactive
        blockBox = GetComponentInChildren<BlockBox>();
        blockBox.ToggleActive(false);

        //calculate gravity
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    protected override void Update()
    {
        //Update our input controller - THIS NEEDS TO BE IN UPDATE TO AVOID RACE CONDITIONS with playerStateController
        playerInputController.Update();    

        //calculate our attempted move amount this frame
        CalculateVelocity();

        //Move us where we need to go
        Move();
    }

    private void LateUpdate()
    {
        //Update our state - THIS NEEDS TO BE IN LATE UPDATE TO AVOID RACE CONDITIONS with playerInputController
        playerStateController.Update();
    }

    public override void CalculateVelocity()
    {
        float targetVelocityX = dirInput.x * moveSpeed * Convert.ToInt32(CanMove);

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;

    }

    public override void Move()
    {
        controller.Move(velocity * Time.deltaTime, dirInput, transform);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }

    public override void TakeDamage(float damageTaken, Vector2 directionHit, bool knockAway = false, float stunTime = 0)
    {
        Debug.Log("Ouch");
        if (IsGrounded)
            TransitionState(PlayerStates.SpearHurt);
        else
            TransitionState(PlayerStates.SpearHurtAir);
    }

    public void TransitionState(PlayerStates state)
    {
        playerStateController.TransitionState(state);
    }

    public void OnActionInput(InputAction action, InputType type)
    {
        playerStateController.OnActionInput(action,type);
    }

    public void BlockDamage(float damageToBlock, Vector2 directionHit, bool knockAway = false, float stunTime = 0)
    {
        Debug.Log("Blocked!");

        if (playerStateController.IsBlocking)
            playerStateController.TransitionState(PlayerStates.SpearBlockedHit);
        else
        {
            Debug.Log("I should not be hit! - this is my state : " + currentStateName);
        }

    }

    public void ParryDamage(float damageToBlock, Vector2 directionHit, bool knockAway = false, float stunTime = 0)
    {
        Debug.Log("Parry!");
    }
}

public enum PlayerStates
{
    //Spear States//
    SpearIdle,
    SpearPivot,

    SpearRunningStart,
    SpearRunning,
    SpearRunningStop,

    SpearJumpStart,
    SpearJumpUp,
    SpearJumpPeak,
    SpearJumpDown,
    SpearJumpLand,

    SpearStabForward,
    SpearStabUp,
    SpearStabForwardBlock,
    SpearStabUpBlock,

    SpearLedgeGrab,

    SpearBlock,
    SpearBlocking,
    SpearBlockedHit,
    SpearBlockMove,
    SpearUnBlock,
    SpearThrow,

    SpearHurt,
    SpearHurtAir,

    SpearDeath,

    //---------------//

    //Missing Spear States//
    Idle,
    Pivot,

    RunningStart,
    Running,
    RunningStop,

    JumpStart,
    JumpUp,
    JumpPeak,
    JumpDown,
    JumpLand,

    StabForward,
    StabUp,

    LedgeGrab,

    Block,
    BlockedHit,
    BlockMove,
    UnBlock,

    Hurt,
    HurtAir,
}

