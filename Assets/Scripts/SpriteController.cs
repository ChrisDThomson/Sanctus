using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    Controller2D controller;

    protected SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        controller = GetComponentInParent<Controller2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        int dir = controller.collisions.faceDir;

        if (dir == 0)
            return;

        Vector3 scale = transform.localScale;
        scale.x = dir;

        transform.localScale = scale;
    }
}
