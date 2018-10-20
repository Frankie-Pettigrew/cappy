using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PedestrianSpawner : MonoBehaviour
{
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}

	#region Singleton

	
	public static PedestrianSpawner Instance;

	private void Awake()
	{
		Instance = this;
	}

	#endregion

	public float clock;
	public float spawnRate;

	public Dictionary<string, Queue<GameObject>> PoolDictionary;
	public static List<GameObject> pedestrianPool = new List<GameObject>();
	public List<Pool> pools;

	public Queue<GameObject> pedestrianQueue;
	
	
	public List<Transform> spawnPoints = new List<Transform>();
	public Transform pedestrianFab;
	private int lastIndex;
	
	// Use this for initialization
	void Start () {
		
		PoolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(false);
				print(obj.transform.position);
				obj.name = obj.name + i;
				objectPool.Enqueue(obj);
				pedestrianPool.Add(obj);
			}
			
			PoolDictionary.Add(pool.tag, objectPool);
		}
		

	}

	public GameObject SpawnFromPool(string tag, Vector3 position)
	{
		GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
		
		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		//print(objectToSpawn.transform.position);
		
		IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

		if (pooledObject != null)
		{
			pooledObject.OnObjectSpawn();
		}
		
		PoolDictionary[tag].Enqueue(objectToSpawn);
//		print(PoolDictionary[tag].Peek().transform.position);
		
		return objectToSpawn;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		PedestrianManager manager = GameObject.Find("Manager").GetComponent<PedestrianManager>();
		spawnRate = manager.spawnSpeed;
		if (clock < spawnRate)
		{
			clock += Time.deltaTime;
		}
		else
		{
			SpawnFromPool("Pedestrian", spawnPoints[Random.Range(0,4)].position);
			clock = 0;
		}
	}
}
