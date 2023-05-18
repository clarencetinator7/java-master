using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  [SerializeField] float moveSpeed = 2f;
  [SerializeField] Transform[] waypoints;
  int waypointIndex = 0;
  Vector2 currentWaypoint;
  Rigidbody2D rb;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    waypointIndex = 0;
    currentWaypoint = waypoints[waypointIndex].position;
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 direction = Vector2.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);

    rb.MovePosition(direction);

    // Checks if the platform reached the currentWaypoint
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
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Debug.Log("Player collided with platform");
      other.transform.SetParent(transform);
    }
  }

  void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Debug.Log("Player exited platform");
      other.transform.SetParent(null);
    }
  }
}
