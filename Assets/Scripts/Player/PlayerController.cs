using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

  // Components
  [SerializeField] Rigidbody2D rb;
  PlayerInputActions playerControls;
  Interactor interactor;

  // Stats
  [SerializeField] float moveSpeed = 5f;
  Vector2 moveDirection = Vector2.zero;
  [SerializeField] float jumpForce = 5f;

  // Input Actions
  InputAction move;
  InputAction jump;
  InputAction interact;
  private void Awake()

  {
    playerControls = new PlayerInputActions();
    interactor = GetComponent<Interactor>();
  }

  private void OnEnable()
  {
    move = playerControls.Player.Move;
    move.Enable();

    jump = playerControls.Player.Jump;
    jump.Enable();
    jump.performed += onJump;

    interact = playerControls.Player.Interact;
    interact.Enable();
    interact.performed += onInteract;

  }
  private void OnDisable()
  {
    move.Disable();
    jump.Disable();
  }

  private void Update()
  {
    moveDirection = move.ReadValue<Vector2>();
  }

  private void FixedUpdate()
  {
    rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
  }

  private void onJump(InputAction.CallbackContext ctx)
  {
    rb.velocity = Vector2.up * jumpForce;
    Debug.Log("Jump");
  }

  private void onInteract(InputAction.CallbackContext ctx)
  {
    Debug.Log("Interact");
    interactor.Interact();
  }
}