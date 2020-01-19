using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// TODO : Abstract this
/// </summary>
public class HurtBoxController : MonoBehaviour
{
    public HurtBox normalHurtBox;
    public HurtBox blockingHurtBox;

    public HashSet<HurtBox> hurtboxes = new HashSet<HurtBox>();

    IDamagable damagable;

    // Start is called before the first frame update
    void Start()
    {
        damagable = GetComponentInParent<IDamagable>();

        int childIndex = transform.childCount;

        for (int i = 0; i < childIndex; i++)
        {
            //get our hurtboxes and set it's damagable 
            HurtBox h = transform.GetChild(i).GetComponent<HurtBox>();
            h.Damagable = damagable;

            hurtboxes.Add(h);
        }
    }

    public void ChangeHurtBox(HurtBoxType type)
    {
        //disable all hurtboxes
        foreach (HurtBox h in hurtboxes)
        {
            h.gameObject.SetActive(false);
        }

        //find which one to activate
        switch (type)
        {
            case HurtBoxType.Normal:
                normalHurtBox.gameObject.SetActive(true);
                break;

            case HurtBoxType.Blocking:
                blockingHurtBox.gameObject.SetActive(true);
                break;
        }
    }

    public enum HurtBoxType
    {
        Normal,
        Blocking,

        NotSet
    }
}