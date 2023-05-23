using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

  [SerializeField] float jumpForce = 25f;
  Rigidbody2D playerRb;
  Animator animator;

  [Header("Audio")]
  [SerializeField] AudioClip jumpSound;

  void Awake()
  {
    animator = GetComponent<Animator>();
  }


  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      animator.SetTrigger("isJump");
      //   Boost(collision.gameObject.GetComponent<Rigidbody2D>());
      playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      playerRb = null;
    }
  }

  public void Boost()
  {
    if (playerRb != null)
    {
      SoundManager.instance.playSound(jumpSound);
      playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
  }

}
