using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;

public class getMidiValues : MonoBehaviour
{
	public float[] tracks = new float[8];
	private int index = 0;

	
	// Update is called once per frame
	void Update ()
	{
		index = 0;
		foreach (float t in tracks)
		{
			tracks[index] = MidiMaster.GetKnob(index, 0);
			index++;
		}
	}
}
