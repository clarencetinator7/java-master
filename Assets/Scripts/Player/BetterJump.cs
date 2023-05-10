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

  public void Start()
  {
    rb = GetComponent<Rigidbody2D>();
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
