using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using UnityEditor;
using UnityEngine.UI;

public class mapManager : MonoBehaviour
{

	public List<Building> buildings = new List<Building>();
	private List<Transform> sidewalks = new List<Transform>();
	List<Transform> roads = new List<Transform>();
	public float sidewalkWidth = 1;

	public GameObject walkerFab;

	public float deltaRefreshTime = 0.5f;
	public float sidewalkWidthDelta;
	private float lastWidth;
	private float lastRefresh;
	public float deltaBarrier = 0.1f;
	public List<Walker> walkers = new List<Walker>();
	public int activePeds;
	public int peoplePerBuilding;

	public int payment = 2;
	public int newHousingPrice = 1;

	private Text housingPrice, houselessPeople, minimumWage;


	public float dayNightSpeed = 1;
	public float dayNightValue = 0;
	private int season;
	private float day;

	
	public bool isNight = true;
	
	public float sunriseTime = 440;
	public float sunsetTime = 999;

	private JunctionController trafficControl;
	
	private float lastSpawn = 0;
	public float spawnRate;

	public changeSynthParams param;
	private getMidiValues midi;
	private JunctionController juncts;
	private cSharpSkyCycle sky;
	private worldRotate rot;
	
	public List<Walker> atWork = new List<Walker>();
	public List<Walker> houseless = new List<Walker>();
	


	// Use this for initialization
	void Start ()
	{
		rot = GetComponentInParent<worldRotate>();
		param = GetComponent<changeSynthParams>();
		midi = GetComponent<getMidiValues>();
		juncts = GameObject.Find("TrafficController").GetComponent<JunctionController>();
		sky = GameObject.Find("SkyDomeDynamic").GetComponent<cSharpSkyCycle>();
		trafficControl = FindObjectOfType<JunctionController>();
		housingPrice = GameObject.Find("Housing Price").GetComponent<Text>();
		houselessPeople = GameObject.Find("Houseless people").GetComponent<Text>();
		minimumWage = GameObject.Find("Minimum Wage").GetComponent<Text>();
		
		createLists();
		createWalkers();
		
	}

	void Update()
	{
		dayNightCycle();
		checkForDeltas();
		//spawnOnTimer();
		slideValues();
		checkForNewDay();
		updateText();
	}

	void updateText()
	{
		housingPrice.text = "Housing Price: " + newHousingPrice;
		houselessPeople.text = "Houseless People " + houseless.Count;
		minimumWage.text = "Minimum Wage: " + payment;
	}

	void checkForNewDay()
	{
		if (sky.day != day)
		{
			changeOnDay();
			day = sky.day;
		}
		else
		{
			day = sky.day;
		}
	}

	void changeOnDay()
	{
		lastSpawn = 0;
		foreach (Building b in buildings)
		{
			if (b.whichBuilding != 0)
			{
				if (b.budget == 0)
				{
					b.whichBuilding = 0;
				}
				else if (b.budget >= b.transformBudget)
				{
					if (b.whichBuilding == 1)
					{
						b.price += 1;
						b.transformBudget += 10;
					} else if (b.whichBuilding == 2)
					{
						b.transformBudget += 20;
					}
				} else if (b.budget == 0)
				{
					b.whichBuilding = 0;
				}
			}
			else
			{
				b.whichBuilding = Random.Range(1, 3);
				b.transformBudget = 10;
				b.budget = 0;
				b.price += 1;
			}
		}
	}
	void slideValues()
	{
		spawnRate = (int)ExtensionMethods.Remap(midi.tracks[0], 0, 1, 10, 80);
		juncts.greenLightTime = ExtensionMethods.Remap(midi.tracks[1], 0, 1, 5, 10);
		param.del2F = ExtensionMethods.Remap(midi.tracks[1], 0, 1, 0.1f, 0.7f);
		payment = (int) ExtensionMethods.Remap(midi.tracks[2], 0, 1, 1, 3);
		newHousingPrice = (int) ExtensionMethods.Remap(midi.tracks[3], 0, 1, 0, 3);

		param.startt1 = ExtensionMethods.Remap(midi.tracks[3], 0, 1, 400, 40000);
		rot.percentRotated = midi.tracks[4];
		param.delay2Wet = ExtensionMethods.Remap(midi.tracks[4], 0, 1, 0.1f, 1f);
		param.synth2R = ExtensionMethods.Remap(midi.tracks[2], 0, 1, 100, 2000);
		param.synth2Volume = midi.tracks[0];
		sidewalkWidth = ExtensionMethods.Remap(midi.tracks[5], 0, 1, 1, 0.5f);
		param.del1F = ExtensionMethods.Remap(midi.tracks[5], 0, 1, 0.2f, 0.9f);

		param.synth1Start = ExtensionMethods.Remap(houseless.Count, 0, 15, 300, 4000);

	}

	void createLists()
	{
		foreach (Building b in GetComponentsInChildren<Building>())
		{
			buildings.Add(b);
			int randy = Random.Range(5, 10);
			b.maxHousing = randy;
			int randySonOfRandy = Random.Range(0, 3);
			b.whichBuilding = randySonOfRandy;
		}

		foreach (GameObject t in GameObject.FindGameObjectsWithTag("Sidewalk"))
		{
			sidewalks.Add(t.transform);
		}

		foreach (GameObject r in GameObject.FindGameObjectsWithTag("Road"))
		{
			roads.Add(r.transform);
		}
	}
	
	public IEnumerator working (int seconds, Walker w) {
		int counter = seconds;
		while (counter > 0)
		{
			w.active = false;
			yield return new WaitForSeconds (1);
			counter--;
		}
		atWork.Remove(w);
		w.worked = true;
		w.budget += payment;
		w.active = true;
		w.gameObject.SetActive(true);
	}


	void dayNightCycle()
	{
		if (sky.min < 480)
		{
			if (sky.min > lastSpawn + (spawnRate * 3))
			{
				lastSpawn = sky.min;
				spawnWalkers();
				
			}
		}
		if (sky.min > 480 && sky.min < 1020)
		{
			if (sky.min > lastSpawn + spawnRate)
			{
				lastSpawn = sky.min;
				spawnWalkers();
			}
		}
		if (sky.min > 1020)
		{
			if (sky.min > lastSpawn + (spawnRate * 2))
			{
				lastSpawn = sky.min;
				spawnWalkers();
			}
		}
	}

	public void spawnWalkers()
	{
		Building mostOccupied = null;
		foreach (Building b in buildings)
		{
			if (mostOccupied != null)
			{
				if (b.whichBuilding == 1)
				{
					if (b.tenantsIn.Count > mostOccupied.tenantsIn.Count)
					{
						mostOccupied = b;
					}
				}
			}
			else
			{
				mostOccupied = b;
			}
		}
		mostOccupied.instantiateWalker();
	}
	

	void createWalkers()
	{
		int index = 0;
		foreach (Building b in buildings)
		{
			for (int i = 0; i < b.maxHousing - 2; i++)
			{
				GameObject newWalker = Instantiate(walkerFab, b.entryPos.position, Quaternion.identity);
				newWalker.transform.parent = b.transform;
				b.tenantsIn.Add(newWalker.GetComponent<Walker>());
				if (Random.value > 0.5f)
				{
					newWalker.GetComponent<Walker>().type = 0;
				}
				else
				{
					newWalker.GetComponent<Walker>().type = 1;
				}

				newWalker.SetActive(false);
				newWalker.name += " " + index;
				index++;
			}
		}
	}
	

	void checkForDeltas()
	{
		
		if (Time.time > lastRefresh + deltaRefreshTime)
		{
			sidewalkWidthDelta = lastWidth - sidewalkWidth;
			lastWidth = sidewalkWidth;
			lastRefresh = Time.time;
			if (sidewalkWidthDelta > deltaBarrier || sidewalkWidthDelta < -deltaBarrier)
			{
				updateParams();
			}
			updateParams();
		}
	}

	void updateParams() //scales the width of each sidewalk and moves the buildings, then rescans the path. 
	{
		float scaleMoveSidewalk = -(10 - sidewalkWidth * 10)/2;
		float scaleMoveBuilding = (sidewalkWidth * 10);
		
		if (sidewalkWidth > 1)
		{
			sidewalkWidth = 1;
		}

		if (sidewalkWidth < 0.5f)
		{
			sidewalkWidth = 0.5f;
		}
		foreach (Transform t in sidewalks) 
		{
			t.localScale = new Vector3(t.localScale.x, t.localScale.y, sidewalkWidth);
			t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, scaleMoveSidewalk);
		}

		foreach (Building b in buildings)
		{
			b.transform.localPosition = new Vector3(b.transform.localPosition.x, b.transform.localPosition.y, scaleMoveBuilding);
		}
		
		AstarPath.active.Scan();
		
	}
}
