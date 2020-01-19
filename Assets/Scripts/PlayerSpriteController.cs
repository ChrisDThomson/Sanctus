using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : SpriteController
{
    public PlayerCharacter character;

    public Spear spear;
    public Transform spearSpawnPos;

    protected override void Start()
    {
        base.Start();

        character = GetComponentInParent<PlayerCharacter>();
    }

    protected override void Update()
    {
        if (!character.CanFlip)
            return;

        int dir = character.controller.Collisions.faceDir;

        if (dir == 0)
            return;

        Vector3 scale = transform.localScale;
        scale.x = dir;

        transform.localScale = scale;
    }

    public void ThrowSpear()
    {
        //Create our spear and call the onThrow to intialize it
        Spear s = spear;
        s.OnThrow(character.controller.Collisions.faceDir);

        //Spawn it in the world - the 
        GameObject g = Instantiate(s.gameObject, null);
        g.transform.position = spearSpawnPos.position;
    }
}
