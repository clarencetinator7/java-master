using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

  // Stats
  [SerializeField] float moveSpeed = 5f;

  // Components
  Rigidbody2D rb;
  Animator animator;
  [SerializeField] Transform[] waypoints;

  // Waypoints
  int waypointIndex = 0;
  Vector2 currentWaypoint;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    waypointIndex = 0;
    currentWaypoint = waypoints[waypointIndex].position;
  }

  void Update()
  {

    Vector2 direction = Vector2.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);

    rb.MovePosition(direction);

    // Checks if the enemy reached the currentWaypoint
    if (Vector2.Distance(transform.position, currentWaypoint) < 0.1f)
    {
      // Checks if the waypointIndex is the last one in the array
      if (waypointIndex == waypoints.Length - 1)
      {
        waypointIndex = 0;
      }
      else
      {
        waypointIndex++;
      }
      // Set the currentWaypoint to the next waypoint in the array
      currentWaypoint = waypoints[waypointIndex].position;
    }

    // Flip sprite on the direction of movement
    if (direction.x > transform.position.x)
    {
      transform.localScale = new Vector3(1, 1, 1);
    }
    else
    {
      transform.localScale = new Vector3(-1, 1, 1);
    }


  }

}
