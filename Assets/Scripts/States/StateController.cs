using Sanctus.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController<T> : IAnimatable<T>
{
    [SerializeField]
    [ReadOnly]
    protected string currentStateName;

    public abstract Animator Animator { get; }

    public abstract void TransitionState(T state);

    // Update is called once per frame
    public abstract void Update();

}
