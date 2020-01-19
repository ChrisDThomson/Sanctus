using UnityEngine;

public class ParryBox : MonoBehaviour, IParryable
{
    IParryable parryable;

    //Needs to happen before start to avoid race conditioning
    public void Awake()
    {
        parryable = gameObject.transform.parent.GetComponentInParent<IParryable>();
    }

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void ParryDamage(float damageTaken, Vector2 directionHit, bool knockAway = false, float stunTime = 0)
    {
        Debug.Assert(parryable != null);

        parryable.ParryDamage(damageTaken, directionHit, knockAway, stunTime);
    }

    public void ToggleActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

}
