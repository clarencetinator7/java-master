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

  // Input Actions
  InputAction move;

  private void Awake()
  {
    playerControls = new PlayerInputActions();
  }

  private void OnEnable()
  {
    move = playerControls.Player.Move;
    move.Enable();
  }

  private void OnDisable()
  {
    move.Disable();
  }

  private void Update()
  {
    moveDirection = move.ReadValue<Vector2>();
  }

  private void FixedUpdate()
  {
    rb.velocity = new Vector2(moveDirection.x * moveSpeed, transform.position.y);
  }


}