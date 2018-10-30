using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class Building : MonoBehaviour
{
	// 0: under construction 1: residential building 2: Store 
	public int whichBuilding = 1;
	private GameObject[] models = new GameObject[3];

	public int price = 1;
	public int budget = 6;
	public int transformBudget = 20;
	
	private mapManager manager;

	private GameObject road;
	private GameObject sidewalk;

	private Material thisMat;

	public Transform entryPos;
	private Vector3 roadPos;
	private Vector3 sidewalkPos;

	public int availableHousing = 20;
	public int maxHousing = 20;
	
	public List<Walker> tenantsIn = new List<Walker>();
	public List<Walker> tenantsOut = new List<Walker>();
	public List<Walker> patrons = new List<Walker>();

	public int numResidents;
	public int numGentrifiers;
	public int numTourists;
	private bool colorChanged;
	private Color lastColor;
	private MeshFilter model;
	


	

	void Start()
	{
		thisMat = GetComponentInChildren<MeshRenderer>().material;
		manager = GetComponentInParent<mapManager>();
		entryPos = transform.GetChild(0);
		road = transform.parent.Find("roadPiece").gameObject;
		sidewalk = transform.parent.Find("sidewalkPiece").gameObject;
		sidewalkPos = sidewalk.transform.position;
		roadPos = road.transform.position;
		initModels();
	
		
	}

	void Update()
	{
		displayModel();
		if (whichBuilding == 1)
		{
			price = manager.newHousingPrice;
		}
	}


	public IEnumerator patronage (int seconds, Walker w) {
		int counter = seconds;
		while (counter > 0)
		{
			w.active = false;
			yield return new WaitForSeconds (1);
			counter--;
		}

		patrons.Remove(w);
		w.eaten = true;
		w.active = true;
		w.gameObject.SetActive(true);
	}

	void displayModel()
	{
		for (int i = 0; i < models.Length; i++)
		{
			if (i == whichBuilding)
			{
				models[i].SetActive(true);
			}
			else
			{
				models[i].SetActive(false);
			}
		}
	}


	void initModels() // grabs children and fills array of models
	{
		for (int i = 0; i < models.Length; i++)
		{
			models[i] = transform.GetChild(i + 1).gameObject;
		}	
	}


	public void instantiateWalker()
	{
		Walker newWalker = tenantsIn[0];
		newWalker.myHome = this;
		newWalker.atHome = false;
		newWalker.worked = false;
		newWalker.eaten = false;
		newWalker.active = true;
		tenantsIn[0].gameObject.SetActive(true);
		tenantsIn.RemoveAt(0);
		tenantsOut.Add(newWalker);
	}

	public void moveOut(Walker w)
	{
		tenantsOut.Remove(w);
		w.myHome = null;
		availableHousing++;
	}

	public void tenantIn(Walker w)
	{
		tenantsIn.Add(w);
		tenantsOut.Remove(w);
		w.myHome = this;
	}
	

	void makeItANewBuilding() // changes model to construction model, after timer sets building to new target
	{
		
	}
}
