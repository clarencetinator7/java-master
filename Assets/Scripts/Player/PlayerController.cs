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
  [SerializeField] Animator animator;
  Collider2D collider;

  // Stats
  [SerializeField] float moveSpeed = 5f;
  Vector2 moveDirection = Vector2.zero;
  [SerializeField] float jumpForce = 5f;
  bool jumpRequest;
  bool isGrounded;
  bool isHurt = false;
  [SerializeField] float kbForce = 5f;

  // Input Actions
  InputAction move;
  InputAction jump;
  InputAction interact;
  private void Awake()
  {
    betterJumpScript = GetComponent<BetterJump>();
    playerControls = new PlayerInputActions();
    interactor = GetComponent<Interactor>();
    animator = GetComponent<Animator>();
    collider = GetComponent<Collider2D>();
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

    // move using transform
    // transform.position += new Vector3(moveDirection.x, 0, 0) * moveSpeed * Time.deltaTime;
    // transform.Translate(new Vector3(moveDirection.x, 0, 0) * moveSpeed * Time.deltaTime);

    // Play animation
    animator.SetFloat("moveX", Mathf.Abs(moveDirection.x));
    // Flip sprite
    if (moveDirection.x > 0)
    {
      // transform.localScale = new Vector3(1, 1, 1);
      GetComponent<SpriteRenderer>().flipX = false;
    }
    else if (moveDirection.x < 0)
    {
      GetComponent<SpriteRenderer>().flipX = true;
      // transform.localScale = new Vector3(-1, 1, 1);
    }
  }

  private void FixedUpdate()
  {
    if (!isHurt)
    {
      rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    if (jumpRequest)
    {
      rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
      jumpRequest = false;
    }

  }

  private void onJump(InputAction.CallbackContext ctx)
  {
    if (ctx.phase == InputActionPhase.Started)
    {
      // Check if grounded
      isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

      if (isGrounded)
      {
        jumpRequest = true;
        betterJumpScript.isStillJumping = true;
      }
    }
    else if (ctx.phase == InputActionPhase.Canceled)
    {
      betterJumpScript.isStillJumping = false;
    }
  }

  private void onInteract(InputAction.CallbackContext ctx)
  {
    // interactor.Interact();
    Knockback();
  }

  public void Hurt(GameObject sender)
  {

    // TODO: DO DAMAGE, PLAY HURT ANIMATION, KNOCKBACK

    // Apply knockback

    isHurt = true;
    animator.SetBool("isHurt", isHurt);
    Knockback();
    StartCoroutine(HurtTimer());


    // Get direction from sender to player
    // Vector2 direction = new Vector2((transform.position.x - sender.transform.position.x), 0).normalized;
  }

  public void Knockback()
  {
    rb.velocity = Vector2.zero;
    rb.sharedMaterial.friction = 0.4f;
    collider.enabled = false;
    collider.enabled = true;
    if (gameObject.GetComponent<SpriteRenderer>().flipX)
    {
      rb.AddForce((Vector2.right + Vector2.up) * kbForce, ForceMode2D.Impulse);
    }
    else
    {
      rb.AddForce((Vector2.left + Vector2.up) * kbForce, ForceMode2D.Impulse);
    }
  }

  IEnumerator HurtTimer()
  {
    yield return new WaitForSeconds(1f);
    isHurt = false;
    rb.sharedMaterial.friction = 0f;
    collider.enabled = false;
    collider.enabled = true;
    animator.SetBool("isHurt", isHurt);
  }





  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    // Draw line
    // Test object direction to world space
  }



}