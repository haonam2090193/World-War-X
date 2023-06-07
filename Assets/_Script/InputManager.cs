using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControl playerControl;

    [SerializeField] private Vector2 movementInput;

    private void OnEnable()
    {
        if(playerControl == null)
        {
            playerControl = new PlayerControl();

            playerControl.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }

}
