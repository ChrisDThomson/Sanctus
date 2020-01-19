using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Hurtboxes contain a damagable that's set by a hurtbox controller
/// we do this to have multiple hurtboxes that all report to it's owner character
/// </summary>
public class HurtBox : MonoBehaviour, IDamagable
{
    IDamagable damagable;

    public IDamagable Damagable
    {
        set { damagable = value; }
    }

    public void TakeDamage(float damageTaken, Vector2 directionHit, bool knockAway = false, float stunTime = 0)
    {
        Debug.Assert(damagable != null);

        damagable.TakeDamage(damageTaken, directionHit, knockAway, stunTime);
    }

}
