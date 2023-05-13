using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{

  // TODO: BETTER JUMP CODE

  public float fallMultiplier = 2.5f;
  public float lowJumpMultiplier = 2f;
  public bool isStillJumping = false;


  Rigidbody2D rb;
  Animator animator;

  public void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    animator.SetBool("isJumping", isStillJumping);
  }

  void FixedUpdate()
  {
    if (rb.velocity.y < 0)
    {
      rb.gravityScale = fallMultiplier;
    }
    else if (rb.velocity.y > 0 && !isStillJumping)
    {
      rb.gravityScale = lowJumpMultiplier;
    }
    else
    {
      rb.gravityScale = 1f;
    }
  }



}
