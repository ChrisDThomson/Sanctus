using UnityEngine;

public class BlockBox : MonoBehaviour, IBlockable
{
    IBlockable blockable;


    //Needs to happen before start to avoid race conditioning
    public void Awake()
    {
        blockable = gameObject.transform.parent.GetComponentInParent<IBlockable>();
    }

    public void BlockDamage(float damageTaken, Vector2 directionHit, bool knockAway = false, float stunTime = 0)
    {
        Debug.Assert(blockable != null);

        blockable.BlockDamage(damageTaken, directionHit, knockAway, stunTime);
    }

    public void ToggleActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
