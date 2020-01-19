using UnityEngine;

public interface IParryable
{
    void ParryDamage(float damageToBlock, Vector2 directionHit, bool knockAway = false, float stunTime = 0);
}
