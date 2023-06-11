using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControl playerControl;
    [SerializeField] AnimationManager animationManager;

    [SerializeField] private Vector2 movementInput;    
    [SerializeField] public float verticalInput;
    [SerializeField] public float horizontalInput;

    private float moveAmount;
    private void Awake()
    {
        animationManager = GetComponent<AnimationManager>();
    }
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

    public void HandleAllInput()
    {
        HandleMovementInput();
    }
    public void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animationManager.UpdateAnimationValues(0, moveAmount);
    }
}
