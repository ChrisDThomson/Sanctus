using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    public Vector2 inputDir;

    // Start is called before the first frame update
    void Start()
    {
        inputDir = Vector2.zero;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
