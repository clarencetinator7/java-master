using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

  // Components
  [SerializeField]
  Rigidbody2D rb;
  PlayerInputActions playerControls;

  // Stats
  [SerializeField]
  float moveSpeed = 5f;
  Vector2 moveDirection = Vector2.zero;
  float jumpForce = 5f;

  // Input Actions
  InputAction move;
  InputAction jump;
  private void Awake()

  {
    playerControls = new PlayerInputActions();
  }

  private void OnEnable()
  {
    move = playerControls.Player.Move;
    move.Enable();

    jump = playerControls.Player.Jump;
    jump.Enable();
    jump.performed += onJump;
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

}