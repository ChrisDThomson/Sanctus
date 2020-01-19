using UnityEngine;

public interface IBlockable
{
    void BlockDamage(float damageToBlock, Vector2 directionHit, bool knockAway = false, float stunTime = 0);
}
