using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using Pathfinding;

public class Walker : MonoBehaviour
{
	// 0 == resident 1 == gentrifier 2 == tourist
	public int type = 0;

	public int budget;
	public bool eaten = false;
	public bool worked = false;
	public bool atHome = true;

	public float distanceTraveledToday = 0;
	private float distanceToCaller;

	private guyAnim anim;
	private Seeker seek;
	public Transform targetPos;
	public float tirednessCap;

	private Path path;
	private int currentWaypoint = 0;
	public bool reachedEndOfPath = false;
	public float nextWaypointDistance = 3;
	public float speed = 2;
	public bool active = false;

	private int whichDude = 0;

	public float repathRate = 0.5f;
	private float lastRepath = float.NegativeInfinity;

	public Building myHome;
	public bool haveHome;

	private bool startGate = false;
	private mapManager manager;
	public bool inJunction;
	public Junction junctIn;
	private bool homeFound = false;
	private Building leastOccupied = null;
	private Building newHome;
	public Building bestUtil;
	

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find("Map Manager").GetComponent<mapManager>();
		seek = GetComponent<Seeker>();
		anim = transform.GetComponentInChildren<guyAnim>();
		seek.pathCallback += OnPathComplete;
//		targetPos = findLeastOccupiedBuilding().transform;
		//seek.StartPath(transform.position, targetPos.position, OnPathComplete);
	}

	private void OnDisable()
	{
		if (startGate)
		{
			seek.pathCallback -= OnPathComplete;
		}
		
	}


	public void goToWork()
	{
		targetPos = GameObject.FindGameObjectsWithTag("exit")[Random.Range(0, 3)].transform;
		reachedEndOfPath = false;
	}

	void Update()
	{
		if (budget < 0)
		{
			budget = 0;
		}

		if (budget > 3)
		{
			budget = 3;
		}
		
		anim.budget = budget;
		if (type == 0)
		{
			anim.GetComponent<SpriteRenderer>().color = Color.green;
		}

		if (type == 1)
		{
			anim.GetComponent<SpriteRenderer>().color = Color.blue;
		}

		if (type == 2)
		{
			anim.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}


	public void findCallers()
	{
		bestUtil = null;
		bool cantAfford = false;
		foreach (Building b in manager.buildings)
		{
			if (bestUtil != null)
			{
				if (b.whichBuilding == 2)
				{
					if (checkUtilValue(b) >= checkUtilValue(bestUtil) &&
					    Vector3.Distance(transform.position, b.transform.position) <
					    Vector3.Distance(transform.position, bestUtil.transform.position))
					{
						bestUtil = b;
						cantAfford = false;
					}
				}
			}
			else
			{
				bestUtil = b;
				cantAfford = true;
			}
		}

		if (!cantAfford)
		{
			targetPos = bestUtil.entryPos;
			reachedEndOfPath = false;
		}
		else
		{
			eaten = true;
			manager.param.lib.SendEvent(Hv_pedSynths_AudioLib.Event.Babang);
		}
	}

	public float checkUtilValue(Building caller)
	{
		if (budget >= caller.price)
		{
			return 1;
		}
		else
		{
			return 0;
		}

		return 0;
	}

	public void goHome()
	{
		if (myHome != null)
		{
			targetPos = myHome.entryPos;
			reachedEndOfPath = false;
		}
	}

	public bool checkPrice()
	{
		if (budget < myHome.price)
		{
			manager.param.lib.SendEvent(Hv_pedSynths_AudioLib.Event.Babang);
			myHome.moveOut(this);
			return false;
		}
		else
		{
			myHome.budget += budget;
			budget = 0;
			myHome.tenantIn(this);
			return true;
		}
	}


	public bool checkPath()
	{
		if (targetPos != null)
		{
			GraphNode node1 = (GraphNode) AstarPath.active.GetNearest(transform.position);
			GraphNode node2 = (GraphNode) AstarPath.active.GetNearest(targetPos.transform.position);
			if (PathUtilities.IsPathPossible(node1, node2))
			{
				return true;
			}
			else
			{
				return false;
				
			}
		}
		else
		{
			return false;
		}
	}

	public void deactive()
	{
		targetPos = null;
		reachedEndOfPath = false;
		active = false;
		gameObject.SetActive(false);
	}

	private void OnCollisionEnter(Collision col)
	{
		
		if (col.transform.CompareTag("Junction"))
		{
			inJunction = true;
			junctIn = col.gameObject.GetComponent<Junction>();
		}
	}

	public void MoveToWaypoint()
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
			anim.walking = true;
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

		Vector3 velocity = new Vector3(0,0,0);
		// Multiply the direction by our desired speed to get a velocity
		//if (checkPath())
		//{
			 velocity = dir * speed * speedFactor;
		//}
		/*else
		{
			 velocity = dir * speed * 0;
		} */

		// actually process the move
		distanceTraveledToday += velocity.magnitude;
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
