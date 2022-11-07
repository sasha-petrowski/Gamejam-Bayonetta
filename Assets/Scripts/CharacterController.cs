using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Character character;

    public float doubleClickTime;

    private float _timeAtLastRight;
    private float _timeAtLastLeft;

    void Update()
    {
        // Movement
        character.HorizontalMovement(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time - _timeAtLastLeft <= doubleClickTime)
            {
                character.DashStart(-1);
            }
            _timeAtLastLeft = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - _timeAtLastRight <= doubleClickTime)
            {
                character.DashStart(1);
            }
            _timeAtLastRight = Time.time;
        }



        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) // JUMP
        {
            Debug.Log("Jump");

            character.JumpStart();
        }

        // Leg
        if (Input.GetKeyDown(KeyCode.Mouse1)) // Grabing the leg
        {
            character.GrabLeg();
        }
        else if(Input.GetKey(KeyCode.Mouse1)) // Holding the leg
        {
            character.HoldLeg();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1)) // Droping the leg
        {
            character.DropLeg();
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            character.AttackStart();
        }
    }
}
