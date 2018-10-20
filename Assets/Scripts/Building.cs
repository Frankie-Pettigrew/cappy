using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	// 0: under construction 1: residential building 2: park 3: storefront 4: bar 
	private int whichBuilding;
	private GameObject[] models;
	
	private mapManager manager;

	private GameObject road;
	private GameObject sidewalk;

	public Vector3 entryPos;
	private Vector3 roadPos;
	private Vector3 sidewalkPos;

	public int availableHousing = 100;
	
	public List<Walker> tenants = new List<Walker>();
	

	void Start()
	{
		manager = GetComponentInParent<mapManager>();
		entryPos = transform.position - new Vector3(0, 0, -manager.sidewalkWidth);
		road = transform.parent.Find("roadPiece").gameObject;
		sidewalk = transform.parent.Find("sidewalkPiece").gameObject;
		sidewalkPos = sidewalk.transform.position;
		roadPos = road.transform.position;
	}



	void initModels() // grabs children and fills array of models
	{
		for (int i = 0; i < models.Length; i++)
		{
			models[i] = transform.GetChild(i).gameObject;
		}	
	}
	

	void makeItANewBuilding() // changes model to construction model, after timer sets building to new target
	{
		
	}
}
