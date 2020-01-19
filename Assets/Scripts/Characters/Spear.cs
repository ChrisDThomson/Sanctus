using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Spear : MonoBehaviour, IMovable
{
    public SpearController2D controller;
    public Vector2 velocity;

    public float moveSpeed = 4;
    public Vector2 moveDir;

    public Animator animator;
    public bool CanMove = true;

    public bool DidSpearHeadCollide
    {
        get
        {
            //choose the side of the box that is the same as our direction
            bool didHitSide = (int)moveDir.x == 1 ?
          controller.collisions.right : controller.collisions.left;

            //we might want the spear to bounce off hills so this is messy for now...
            if (didHitSide || controller.collisions.climbingSlope
                || controller.collisions.below || controller.collisions.below)
                return true;
            else
                return false;
        }
    }


    SpearStateController spearStateController;

    void OnEnable()
    {
        spearStateController = new SpearStateController(this);

        TransitionState(SpearStates.spearThrown);
    }


    // Update is called once per frame
    void Update()
    {
        spearStateController.Update();

        CalculateVelocity();
        Move();
    }

    public void OnThrow(int dir)
    {
        moveDir.x = dir;
    }

    public void CalculateVelocity()
    {
        velocity.x = moveSpeed * (int)moveDir.x * Convert.ToInt32(CanMove);
    }

    public void Move()
    {
        Debug.Log(gameObject.transform);
        controller.Move(velocity * Time.deltaTime, Vector2.zero, transform);
    }

    public void TransitionState(SpearStates state)
    {
        spearStateController.TransitionState(state);
    }
}

public enum SpearStates
{
    spearThrown,
    spearWallHit,
    spearBounce,
}



