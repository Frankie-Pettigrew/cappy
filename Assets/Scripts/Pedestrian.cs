using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Transform))]


public class Pedestrian : MonoBehaviour, IPooledObject
{
	private int behaviour;
	private int directionality;
	public float speed = 0.05f;
	private float talkCounter;
	public float distToTravel;
	public float TalkTime = 1f;
	public GameObject music;
	changeSynthParams parms;
	private Rigidbody rb;
	public float bumpForce = 2f;
	private Hv_pedSynths_AudioLib lib;
	public PedestrianManager manager;
	private Vector3 otherTransform;
	public List<GameObject> knownPedestrians = new List<GameObject>();
	public int numKnownPeds = 20;
	public bool collided;
	public bool isTalking, startTalking;
	public bool isTalker;
	public Pedestrian yourTalker;
	private guyAnim anim;
	private Vector3 lastPos;
	public float deltaPosAmt = 0.1f;

	public PickReactionImage reaction;

	public GameObject closestPed;

	public Pedestrian closePedScr;
	//private Hv_bangSynth_AudioLib audioLib;


	public void OnObjectSpawn ()
	{
		rb = GetComponent<Rigidbody>();
		if (rb == null)
		{
			Debug.LogError("Where's ur rb dude?");
		}
		pointInDirection();
		isTalking = false;
		isTalker = false;
		yourTalker = null;
		transform.rotation = Quaternion.identity;
		manager = GameObject.Find("Manager").GetComponent<PedestrianManager>();
		//audioLib = manager.gameObject.GetComponent<Hv_bangSynth_AudioLib>();
		speed = manager.speed;
		distToTravel = manager.distToTravel;
		manager.activePedestrians++;
	}
	
	void Update () {
		//moveInDirection();
		if ((transform.position - lastPos).magnitude > deltaPosAmt)
		{
			anim.walking = true;
			anim.singing = false;
		}
		else
		{
			anim.singing = true;
			anim.walking = false;
		}

		lastPos = transform.position;

	}

	void Start()
	{
		music = GameObject.FindGameObjectWithTag("music");
		lib = music.GetComponent<Hv_pedSynths_AudioLib>();
		parms = music.GetComponent<changeSynthParams>();
		anim = GetComponent<guyAnim>();
		createKnown();
		reaction = transform.Find("Cloud").transform.Find("Reaction").GetComponentInChildren<PickReactionImage>();
	}

	public void startTalk()
	{
		if (isTalker)
		{
			//isTalking = true;
			//closePedScr.isTalking = true;
			startTalking = true;
			closePedScr.startTalking = true;
		}
	}

	public bool checkIfOnScreen()
	{
		if (isActiveAndEnabled && Camera.main.WorldToViewportPoint(transform.position).x > 0 && Camera.main
			                                                               .WorldToViewportPoint(transform.position).x <
		                                                               1
		                                                               && Camera.main
			                                                               .WorldToViewportPoint(transform.position).y >
		                                                               0 && Camera.main
			                                                               .WorldToViewportPoint(transform.position).y <
		                                                               1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	

	public GameObject checkForFriends()
	{
		List<GameObject> onScreenPeds = new List<GameObject>();
		if (checkIfOnScreen())
		{
			foreach (var t in knownPedestrians)
			{
				if (t.GetComponent<Pedestrian>().checkIfOnScreen())
				{
					onScreenPeds.Add(t);
				}
				else
				{
					onScreenPeds.Remove(t);
				}
			}

			if (onScreenPeds.Count > 0)
			{
				foreach (var j in onScreenPeds)
				{
					if (closestPed != null)
					{
						if ((transform.position - j.transform.position).magnitude <
						    (transform.position - closestPed.transform.position).magnitude)
						{
							closestPed = j;
							closePedScr = closestPed.GetComponent<Pedestrian>();
						}
					}
					else
					{
						closestPed = j;
						closePedScr = closestPed.GetComponent<Pedestrian>();
					}
				}
			}
			else
			{
				return null;
			}

					//return null;
		}
		else
		{
			Debug.LogError("They weren't on screen");
			return null;
		}
		
		if (closestPed != null/* && !closePedScr.isTalking*/)
		{
			return closestPed;
		}
		else
		{
			Debug.LogError("uh it broke? no closest ped or anything");
			return null;
		}
	}

	void createKnown() // Creates a list of known pedestrians, unless the index spits out its own name
	{
		for (int i = 0; i < numKnownPeds; i++)
		{
			int newIndex = Random.Range(0, PedestrianSpawner.pedestrianPool.Count);
			if (PedestrianSpawner.pedestrianPool[newIndex].gameObject.name != gameObject.name)
			{
				knownPedestrians.Add(PedestrianSpawner.pedestrianPool[newIndex]);
			}
			
		}
	}

	public bool moveInDirection() //takes directionality, makes the pedestrian move until they hit the opposing spawn point
	{
		if (!startTalking)
		{
			if (directionality == 0)
			{
				if (transform.position.z > -7)
				{
					transform.position += new Vector3(0, 0, -speed);
					return false;
				}
				else
				{
					return true;
				}
			}

			if (directionality == 1)
			{
				if (transform.position.z > -7)
				{
					transform.position += new Vector3(0, 0, -speed);
					return false;
				}
				else
				{
					return true;
				}
			}

			if (directionality == 2)
			{

				if (transform.position.z < 7)
				{
					transform.position += new Vector3(0, 0, speed);
					return false;
				}
				else
				{
					return true;
				}
			}

			if (directionality == 3)
			{
				if (transform.position.z < 7)
				{
					transform.position += new Vector3(0, 0, speed);
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}

		return true;
	}

	public int checkReaction() // checks if the two talking pedestrians know each other, spits out either both of them know each other or one person is confused and the other is angry
	{
		if (isTalker)
		{
			//Debug.Log("Got here");
			if (closePedScr.closestPed != null)
			{
				if (closePedScr.closestPed.name == gameObject.name)
				{
					Debug.Log("You knew em, and they knew u");
					return 0;
				}
				Debug.Log("You knew em, but they didn't know u");
				return 1;
			}
			Debug.Log("You knew em, but they didn't know u");
			return 1;
		}

		if (closestPed == null) return 2;
		if (closestPed.name == yourTalker.gameObject.name && closePedScr.isTalking)
		{
			Debug.Log("You knew em, and they knew u");
			return 0;
		}
		Debug.Log("who was that");
		return 2;
	}

	public void displayReaction(int value)
	{
		//Debug.Log("Sprite Value: " + value);
		reaction.displaySprite(value);

		if (value == 0)
		{
			parms.synth1A += 10;
			parms.synth1End -= 50;
			parms.synth1D += 10;
			parms.del1T -= 10;
			parms.del1F -= 0.05f;
			lib.SendEvent(Hv_pedSynths_AudioLib.Event.Babang);

		}
		if (value == 1)
		{
			parms.synth1A -= 10;
			parms.startt1 += 5;
			parms.endd1 -= 5;
			parms.timme1 += 5;
			parms.synth1D += 10;
			parms.del1T += 10;
			parms.del1F += 0.01f;
			lib.SendEvent(Hv_pedSynths_AudioLib.Event.Babang);
		}
		if (value == 2)
		{
			parms.synth1A -= 10;
			parms.startt1 -= 5;
			parms.timme1 -= 5;
			parms.synth1End -= 50;
			parms.synth1D -= 10;
			parms.del1T += 20;
			parms.del1F += 0.01f;
			lib.SendEvent(Hv_pedSynths_AudioLib.Event.Babang);
		}
	}
	
	

	void pointInDirection() // Finds out what spawn point the pedestrian started at
	{
		if (transform.position.x == -7 && transform.position.z == 7)
		{
			directionality = 0;
		} else if (transform.position.x == 7 && transform.position.z == 7)
		{
			directionality = 1;
		} else if (transform.position.x == 7 && transform.position.z == -7)
		{
			directionality = 2;
		}  else if (transform.position.x == -7 && transform.position.z == -7)
		{
			directionality = 3;
		}
	}
	
}
