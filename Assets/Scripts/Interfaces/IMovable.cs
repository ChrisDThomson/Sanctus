using UnityEngine;


public interface IMovable
{
    void Move();
    void CalculateVelocity();
}

public interface IAnimatable<T>
{
    Animator Animator { get;}
    void TransitionState(T state);
}

