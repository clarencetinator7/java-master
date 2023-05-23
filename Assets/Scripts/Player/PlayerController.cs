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
  Collider2D boxCollider;

  // Stats
  [SerializeField] float moveSpeed = 5f;
  Vector2 moveDirection = Vector2.zero;
  [SerializeField] float jumpForce = 5f;
  bool jumpRequest;
  bool isGrounded;
  public bool isHurt = false;
  [SerializeField] float kbForce = 5f;

  // Input Actions
  InputAction move;
  InputAction jump;
  InputAction interact;

  [Header("Audio")]
  [SerializeField] AudioClip jumpSound;
  [SerializeField] AudioClip hurtSound;

  [Header("Particles")]
  [SerializeField] GameObject hurtParticle;

  public Collider2D Collider { get => boxCollider; set => boxCollider = value; }

  private void Awake()
  {
    betterJumpScript = GetComponent<BetterJump>();
    playerControls = new PlayerInputActions();
    interactor = GetComponent<Interactor>();
    animator = GetComponent<Animator>();
    Collider = GetComponent<Collider2D>();
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
  private void OnDestroy()
  {
    move.Disable();
    // jump.started -= onJump;
    // jump.canceled -= onJump;
    jump.Disable();
    // interact.performed -= onInteract;
    interact.Disable();
  }

  private void Update()
  {
    moveDirection = move.ReadValue<Vector2>();
    // Play animation
    animator.SetFloat("moveX", Mathf.Abs(moveDirection.x));
    // Flip sprite
    if (moveDirection.x > 0)
    {
      GetComponent<SpriteRenderer>().flipX = false;
    }
    else if (moveDirection.x < 0)
    {
      GetComponent<SpriteRenderer>().flipX = true;
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
        // Play sound
        SoundManager.instance.playSound(jumpSound);
      }
    }
    else if (ctx.phase == InputActionPhase.Canceled)
    {
      betterJumpScript.isStillJumping = false;
    }
  }

  private void onInteract(InputAction.CallbackContext ctx)
  {
    interactor.Interact();
  }



  public void switchActMap(string mode)
  {
    if (mode == "enable")
    {
      move.Enable();
      jump.Enable();
      // interact.Enable();
      Debug.Log("Enabled player controls");

      // COULD ALSO SWITCH TO UI MAP
    }
    else if (mode == "disable")
    {
      move.Disable();
      jump.Disable();
      // interact.Disable();
      Debug.Log("Disabled player controls");
    }
  }

  public void Hurt(GameObject sender)
  {
    isHurt = true;
    animator.SetBool("isHurt", isHurt);
    // Play hurt sound
    SoundManager.instance.playSound(hurtSound);

    // Spawn hurt particle
    GameObject particle = Instantiate(hurtParticle, transform.position, Quaternion.identity);
    Destroy(particle, 1f);
    // Reduce life
    GameManager.instance.ReduceLife();
    // Get direction from sender to player
    Vector2 direction = new Vector2((transform.position.x - sender.transform.position.x), 0).normalized;
    Knockback(direction);
    StartCoroutine(HurtTimer());
  }

  IEnumerator HurtTimer()
  {
    yield return new WaitForSeconds(1f);
    isHurt = false;
    rb.sharedMaterial.friction = 0f;
    Collider.enabled = false;
    Collider.enabled = true;
    animator.SetBool("isHurt", isHurt);
  }

  public void Knockback(Vector2 kbDir)
  {
    rb.velocity = Vector2.zero;
    rb.sharedMaterial.friction = 0.4f;
    Collider.enabled = false;
    Collider.enabled = true;

    rb.AddForce(new Vector2(kbDir.x, 1f) * kbForce, ForceMode2D.Impulse);
  }

}