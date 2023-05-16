using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
  public void Interact(GameObject interactor);
}

public class Interactor : MonoBehaviour
{

  // Components
  [SerializeField] Transform interactionPoint;

  // Stats
  [SerializeField] float interactionRadius = 1f;

  public void Interact()
  {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(interactionPoint.position, interactionRadius);

    foreach (Collider2D collider in colliders)
    {
      // IInteractable interactable = collider.GetComponent<IInteractable>();
      // if (interactable != null)
      // {
      //     interactable.Interact();
      // }
      if (collider.CompareTag("Interactable"))
      {
        // GetComponent<PlayerController>().switchActMap("disable");
        collider.GetComponent<IInteractable>().Interact(gameObject);
      }
    }
  }


  // SAVE: Interaction range
  // private void OnDrawGizmos()
  // {
  //   Gizmos.color = Color.red;
  //   Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
  // }

}
