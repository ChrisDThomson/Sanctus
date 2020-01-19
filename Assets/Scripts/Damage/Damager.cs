using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    new Collider2D collider;

    public LayerMask layerMask;

    public float damage = 0;
    public float stunTime = 0;
    public bool doesKnockAway = false;
    public bool isMultiHit = false; // TODO : Implement multihit functionality
    //TODO : add a timer to control time between hits

    public bool willDestroyOnBlock;
    public bool canParry = true;

    public delegate void OnParryDel(Vector2 directionHit);
    public OnParryDel OnParry;

    HashSet<Collider2D> damagedColliders = new HashSet<Collider2D>();
    HashSet<Collider2D> oldOverlappedColliders = null;

    delegate Collider2D[] GetOverlapCollidersFunc();
    GetOverlapCollidersFunc getOverlapCollidersFunc;

    bool didHit = false;
    bool flaggedToDestroy = false;

    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<Collider2D>();

        if (collider.GetType() == typeof(BoxCollider2D))
        {
            getOverlapCollidersFunc = GetCollidersOverlapBox;
        }

        if (collider.GetType() == typeof(CircleCollider2D))
        {
            getOverlapCollidersFunc = GetCollidersOverlapCircle;
        }

        if (collider.GetType() == typeof(CapsuleCollider2D))
        {
            getOverlapCollidersFunc = GetCollidersOverlapCapsule;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (flaggedToDestroy)
            return;

        Collider2D[] colliders = getOverlapCollidersFunc();

        foreach (Collider2D c in colliders)
        {         
            if (!oldOverlappedColliders.Contains(c) && damagedColliders.Contains(c))
                damagedColliders.Remove(c);

            if (!damagedColliders.Contains(c))
            {
                //if (!c.gameObject.activeSelf)
                //    continue;

                IParryable parryable = c.GetComponent<IParryable>();

                if (parryable != null)
                {
                    Vector2 dir = c.transform.position - collider.transform.position;
                    dir.Normalize();

                    parryable.ParryDamage(damage, dir, doesKnockAway, stunTime);

                    //add it to the list of damaged colliders, so it wont get called again
                    damagedColliders.Add(c);

                    if (OnParry != null)
                        OnParry(-dir);

                    //skip over damage check
                    break;
                }

                //if we've hit a block box 
                IBlockable blockable = c.GetComponent<IBlockable>();

                if (blockable != null)
                {

                    Vector2 dir = c.transform.position - collider.transform.position;
                    dir.Normalize();

                    blockable.BlockDamage(damage, dir, doesKnockAway, stunTime);

                    //add it to the list of damaged colliders, so it wont get called again
                    damagedColliders.Add(c);

                    if (willDestroyOnBlock)
                        flaggedToDestroy = true;

                    //skip over damage check
                    break;
                }

                IDamagable damagable = c.GetComponent<IDamagable>();

                if (damagable != null)
                {
                    Vector2 dir = c.transform.position - collider.transform.position;
                    dir.Normalize();

                    damagable.TakeDamage(damage, dir, doesKnockAway, stunTime);
                    damagedColliders.Add(c);
                }
            }
        }

        oldOverlappedColliders = new HashSet<Collider2D>(colliders);

        if (flaggedToDestroy)
            Destroy(gameObject);

    }

    void OnEnable()
    {
        damagedColliders.Clear();
    }

    Collider2D[] GetCollidersOverlapBox()
    {
        return Physics2D.OverlapBoxAll(collider.bounds.center, collider.bounds.size, transform.rotation.z, layerMask);
    }

    Collider2D[] GetCollidersOverlapCircle()
    {
        CircleCollider2D col = (CircleCollider2D)collider;

        return Physics2D.OverlapCircleAll(col.bounds.center, col.radius, layerMask);
    }

    Collider2D[] GetCollidersOverlapCapsule()
    {
        CapsuleCollider2D col = (CapsuleCollider2D)collider;

        return Physics2D.OverlapCapsuleAll(col.bounds.center, col.bounds.size, col.direction, transform.rotation.z, layerMask);
    }
}
