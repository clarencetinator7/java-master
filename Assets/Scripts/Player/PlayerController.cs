using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

  // Components
  [SerializeField] Rigidbody2D rb;
  BetterJump betterJumpScript;
  PlayerInputActions playerControls;
  Interactor interactor;
  [SerializeField] Transform groundCheck;
  [SerializeField] LayerMask groundLayer;


  // Stats
  [SerializeField] float moveSpeed = 5f;
  Vector2 moveDirection = Vector2.zero;
  [SerializeField] float jumpForce = 5f;
  bool jumpRequest;
  bool isGrounded;

  // Input Actions
  InputAction move;
  InputAction jump;
  InputAction interact;
  private void Awake()
  {
    betterJumpScript = GetComponent<BetterJump>();
    playerControls = new PlayerInputActions();
    interactor = GetComponent<Interactor>();
  }

  private void OnEnable()
  {
    move = playerControls.Player.Move;
    move.Enable();

    jump = playerControls.Player.Jump;
    jump.Enable();
    jump.started += onJump;
    jump.canceled += onJump;

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

    if (jumpRequest)
    {
      // rb.velocity += Vector2.up * jumpForce;
      rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
      jumpRequest = false;
    }

  }

  private void onJump(InputAction.CallbackContext ctx)
  {
    if (ctx.phase == InputActionPhase.Started)
    {
      // rb.velocity = Vector2.up * jumpForce;
      // jumpRequest = true;
      // Check if grounded
      isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

      if (isGrounded)
      {
        jumpRequest = true;
        betterJumpScript.isStillJumping = true;
      }
      Debug.Log("Started");
    }
    else if (ctx.phase == InputActionPhase.Canceled)
    {
      betterJumpScript.isStillJumping = false;
      Debug.Log("Canceled");
    }
  }

  private void onInteract(InputAction.CallbackContext ctx)
  {
    Debug.Log("Interact");
    interactor.Interact();
  }
}