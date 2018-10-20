using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldRotate : MonoBehaviour
{
	private Vector3 startRot;
	private Vector3 endRot;
	public float percentRotated = 0;
	
	// Use this for initialization
	void Start ()
	{
		startRot = transform.eulerAngles;
		endRot = new Vector3(transform.eulerAngles.x, 359, transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.eulerAngles = Vector3.Lerp(startRot, endRot, percentRotated);
	}
}
