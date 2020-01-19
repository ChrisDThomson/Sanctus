using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damageTaken, Vector2 directionHit, bool knockAway = false, float stunTime = 0);
}
