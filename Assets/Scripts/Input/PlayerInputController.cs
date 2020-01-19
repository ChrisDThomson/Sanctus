using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController
{
    public PlayerCharacter playerCharacter;

    /// <summary>
    /// Action 1 = Attack
    /// Action 2 = Block
    /// Action 3 = Throw
    /// Action 4 = Jump
    /// </summary>
    // Update is called once per frame
    public void Update()
    {
        if (playerCharacter != null)
        {
            playerCharacter.dirInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); ;


            //-----------------------------------//

            //Attacking
            if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("Action1"))
            {
                playerCharacter.OnActionInput(InputAction.Action1, InputType.Pressed);
            }

            if (Input.GetKeyUp(KeyCode.K) || Input.GetButtonUp("Action1"))
            {
                playerCharacter.OnActionInput(InputAction.Action1, InputType.Released);
            }

            //-----------------------------------//




            //-----------------------------------//

            Debug.Log("hey llook at mesdasdasdas");

            //Blocking
            if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Action2"))
            {
                playerCharacter.OnActionInput(InputAction.Action2, InputType.Pressed);
            }

            if (Input.GetKeyUp(KeyCode.L) || Input.GetButtonUp("Action2"))
            {
                playerCharacter.OnActionInput(InputAction.Action2, InputType.Released);
            }

            if (Input.GetKey(KeyCode.L) || Input.GetButton("Action2"))
            {
                Debug.Log("hey llook at me");
                playerCharacter.OnActionInput(InputAction.Action2, InputType.Held);
            }

            //-----------------------------------//




            //-----------------------------------//

            //Throwing
            if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Action3"))
            {
                playerCharacter.OnActionInput(InputAction.Action3, InputType.Pressed);
            }

            if (Input.GetKeyUp(KeyCode.O) || Input.GetButtonUp("Action3"))
            {
                playerCharacter.OnActionInput(InputAction.Action3, InputType.Released);
            }

            //-----------------------------------//




            //-----------------------------------//
            //Jumping
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Action4"))
            {
                playerCharacter.OnActionInput(InputAction.Action4, InputType.Pressed);
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Action4"))
            {
                playerCharacter.OnActionInput(InputAction.Action4, InputType.Released);
            }

            //-----------------------------------//
        }
    }
}

public enum InputAction
{
    Action1,
    Action2,
    Action3,
    Action4,
}


public enum InputType
{
    Pressed,
    Released,
    Held,
}

