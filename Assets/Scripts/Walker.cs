using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using Pathfinding;

public class Walker : MonoBehaviour
{
	private guyAnim anim;
	private Seeker seek;
	public Transform targetPos;

	private Path path;
	private int currentWaypoint = 0;
	public bool reachedEndOfPath = false;
	public float nextWaypointDistance = 3;
	public float speed = 2;

	private int whichDude = 0;

	public float repathRate = 0.5f;
	private float lastRepath = float.NegativeInfinity;

	public Building myHome;

	private bool startGate = false;
	

	// Use this for initialization
	void Start ()
	{
		seek = GetComponent<Seeker>();
		anim = GetComponent<guyAnim>();
		seek.pathCallback += OnPathComplete;
		seek.StartPath(transform.position, targetPos.position, OnPathComplete);
	}

	private void OnDisable()
	{
		if (startGate)
		{
			seek.pathCallback -= OnPathComplete;
		}
		
	}

	void Update()
	{
		
	}

	void MoveToWaypoint()
	{
		if (Time.time > lastRepath + repathRate && seek.IsDone()) {
			lastRepath = Time.time;

			// Start a new path to the targetPosition, call the the OnPathComplete function
			// when the path has been calculated (which may take a few frames depending on the complexity)
			seek.StartPath(transform.position, targetPos.position, OnPathComplete);
		}

		if (path == null)
		{
			// We have no path to follow, so return
			return;
		}

		// check in a loop if we're close enough to the waypoint to switch to the next one
		// we do this because many of them will be close enough to reach in the same frame
		reachedEndOfPath = false;

		// distance to the next waypoint in the path
		float distanceToWaypoint;

		while (true)
		{ 
			// If you want maximum performance you can check the squared distance instead to get rid of a
			// square root calculation. But that is outside the scope of this tutorial.
			
			distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
			if (distanceToWaypoint < nextWaypointDistance)
			{
				// Check if there is another waypoint or if we have reached the end of the path
				if (currentWaypoint + 1 < path.vectorPath.Count)
				{
					currentWaypoint++;
				}
				else
				{
					// Set a status variable to indicate that the agent has reached the end of the path.
					// You can use this to trigger some special code if your game requires that.
					reachedEndOfPath = true;
					break;
				}
			}
			else
			{
				break;
			}

		}

		// Slow down smoothly upon approaching the end of the path
		// This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
		float speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

		// Direction to the next waypoint
		// Normalize it so that it has a length of 1 world unit
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

		// Multiply the direction by our desired speed to get a velocity
		Vector3 velocity = dir * speed * speedFactor;

		// actually process the move
		transform.position += velocity * Time.deltaTime;



	}


	public void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
		Debug.Log("We got a path back, did it return an error? " + p.error);
	}

}
