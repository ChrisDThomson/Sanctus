using Sanctus.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public abstract class Character<T> : MonoBehaviour, IDamagable, IMovable
{
    [SerializeField]
    [ReadOnly]
    public Vector2 dirInput;

    public T controller;
    //public CharacterInputController characterInput;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        controller = GetComponent<T>();
        //characterInput = GetComponent<CharacterInputController>();
    }

    // Update is called once per frame
    protected abstract void Update();

    public abstract void CalculateVelocity();

    public abstract void Move();

    public abstract void TakeDamage(float damageTaken, Vector2 directionHit, bool knockAway = false, float stunTime = 0);
}
