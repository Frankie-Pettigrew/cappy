﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

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


	public float dayNightSpeed = 1;
	public float dayNightValue = 0;
	private int season;
	
	public bool isNight = true;
	
	public float sunriseTime = 440;
	public float sunsetTime = 999;

	private JunctionController trafficControl;
	


	// Use this for initialization
	void Start ()
	{
		trafficControl = FindObjectOfType<JunctionController>();
		
		createLists();
		createWalkers();
		
	}

	void Update()
	{
		dayNightCycle();
		checkForDeltas();
		spawnOnTimer();
	}

	void createLists()
	{
		foreach (Building b in GetComponentsInChildren<Building>())
		{
			buildings.Add(b);	
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

	void spawnFromMostOccupiedBuilding()
	{
		int mostOccupiedCount = 0;
		Building mostOccupied = buildings[0];
		foreach (Building b in buildings)
		{
			if (b.tenantsIn.Count > mostOccupiedCount)
			{
				mostOccupiedCount = b.tenantsIn.Count;
				mostOccupied = b;
			}
		}
		mostOccupied.instantiateWalker();
	}

	void dayNightCycle()
	{
		if (dayNightValue < 1440)
		{
			if (dayNightValue < sunriseTime)
			{
				isNight = true;
			} else if (dayNightValue < sunsetTime)
			{
				isNight = false;
			}
			dayNightValue += Time.time * 60 * dayNightSpeed;
		}
		else
		{
			dayNightValue = 0;
		}
		
	}

	void spawnOnTimer()
	{
		if (isNight)
		{
			if (dayNightValue % 3 == 0)
			{
				spawnFromMostOccupiedBuilding();
			}
		}
		else
		{
			if (dayNightValue == 0)
			{
				spawnFromMostOccupiedBuilding();
			}
		}
	}
	

	void createWalkers()
	{
		foreach (Building b in buildings)
		{
			for (int i = 0; i < b.availableHousing; i++)
			{
				GameObject newWalker = Instantiate(walkerFab, b.entryPos, Quaternion.identity);
				newWalker.transform.parent = b.transform;
				b.tenantsIn.Add(newWalker.GetComponent<Walker>());
				newWalker.SetActive(false);
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
