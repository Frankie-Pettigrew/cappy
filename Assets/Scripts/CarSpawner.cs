using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Scripting.Pipeline;

public class CarSpawner : MonoBehaviour {
	
	public List<GameObject> cars = new List<GameObject>();
	public int carPoolSize = 10;
	public GameObject carFab;
	public WaypointsHolder holder;
	public Junction myJunct;
	public int spawnsPerLight = 2;
	private bool spawning = false;
	int carsSpawned = 0;
	private bool turned;

	public float spawnDelay = 0.5f;
	public float spawnTimer = 0;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < carPoolSize; i++)
		{
			GameObject newCar = Instantiate(carFab, transform.position, Quaternion.identity);
			newCar.transform.parent = transform;
			cars.Add(newCar);
			newCar.GetComponent<CarAI>().waypointsHolder = holder.transform;
			newCar.SetActive(false);
			myJunct = transform.parent.GetComponent<Junction>();
			cars[0].SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		spawnCars();
	}

	void spawnCars()
	{
		if (myJunct.free && spawning)
		{
			if (spawnTimer < spawnDelay)
			{
				spawnTimer += Time.deltaTime;
			}
			else if(carsSpawned < spawnsPerLight)
			{
				Debug.Log("got here");
				CarAI newCar = findNonActiveCar().GetComponent<CarAI>();
				newCar.gameObject.SetActive(true);
				newCar.Restart();
				carsSpawned++;
				spawnTimer = 0;
			}
		}
		else if(myJunct.free && turned)
		{
			Debug.Log("changing the light, spawning new cars");
			spawning = true;
			turned = false;
			carsSpawned = 0;
		}
		else if (!myJunct.free)
		{
			Debug.Log("the light is red");
			spawning = false;
			turned = true;
		}
	}

	GameObject findNonActiveCar()
	{
		GameObject retObj = null;
		foreach (GameObject c in cars)
		{
			if (!c.activeSelf)
			{
				retObj = c;
			}
		}

		if (retObj != null)
		{
			Debug.Log(retObj.name);
			return retObj;
		}

		Debug.LogError("No car found");
		return null;
	}
}
