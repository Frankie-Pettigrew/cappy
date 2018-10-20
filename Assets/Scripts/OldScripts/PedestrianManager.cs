using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class PedestrianManager : MonoBehaviour
{
	public GameObject spawner;
	
	public float speed;
	public float spawnSpeed;
	private getMidiValues midi;
	private changeSynthParams parms;
	private worldRotate rotator;

	public float distToTravel;
//	private Hv_bangSynth_AudioLib musicLib;
	public int activePedestrians = 0;
	public GameObject music;
	private RotateLight light;
	private Light dirLight;
	public Color startCol, endCol;
	private float colTween = 0;

	public List<GameObject> texts = new List<GameObject>();
	


	void Start()
	{
		rotator = GameObject.Find("world").GetComponent<worldRotate>();	
		light = GameObject.Find("Directional Light").GetComponent<RotateLight>();
		dirLight = light.gameObject.GetComponent<Light>();
		dirLight.color = startCol;
		music = GameObject.FindGameObjectWithTag("music");
		parms = music.GetComponent<changeSynthParams>();
		midi = music.GetComponent<getMidiValues>();
		spawner = GameObject.Find("PeopleSpawner");
		texts.Add(GameObject.Find("Pedestrian Spawn Speed"));
		texts.Add(GameObject.Find("Bump Strength"));
		texts.Add(GameObject.Find("Walk Speed"));
	//	musicLib = GetComponent<Hv_bangSynth_AudioLib>();
	//	musicLib.SendEvent(Hv_bangSynth_AudioLib.Event.Delayon);
	//	musicLib.SetFloatParameter(Hv_bangSynth_AudioLib.Parameter.Synth1q, 0.2f);
	}

	void Update()
	{
		Color newCol = Color.Lerp(startCol, endCol, midi.tracks[2]);
		spawnSpeed = ExtensionMethods.Remap(midi.tracks[0], 0, 1, 2, 0.1f);
		speed = ExtensionMethods.Remap(midi.tracks[1], 0, 1, 0.05f, 0.3f);
		distToTravel = ExtensionMethods.Remap(midi.tracks[2], 0, 1, 0.05f, 0.3f);
		parms.synth2A = ExtensionMethods.Remap(midi.tracks[3], 0, 1, 10, 2000);
		parms.startt1 = ExtensionMethods.Remap(midi.tracks[4], 0, 1, 0, 4000);
		light.rotSpeed = ExtensionMethods.Remap(midi.tracks[4], 0, 1, 0.1f, 3);
		parms.del2F = ExtensionMethods.Remap(midi.tracks[5], 0, 1, 0.01f, 0.95f);
		rotator.percentRotated = midi.tracks[6];
		dirLight.color = newCol;

	}


}
