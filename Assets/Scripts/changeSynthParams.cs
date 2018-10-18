using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class changeSynthParams : MonoBehaviour
{
	private Hv_pedSynths_AudioLib lib;
	
	public float del1F,
		del1T,
		del2F,
		del2T,
		delay1Wet,
		delay2Wet,
		endd1,
		qq2,
		rootnote,
		speed,
		startt1,
		synth1A,
		synth1D,
		synth1End,
		synth1Q,
		synth1R,
		synth1S,
		synth1Start,
		synth1Vol,
		synth2A,
		synth2D,
		synth2Octave,
		synth2R,
		synth2S,
		synth2Volume,
		synthOctave1,
		timme1,
		volume;
	
	// Use this for initialization
	void Start ()
	{
		lib = GetComponent<Hv_pedSynths_AudioLib>();
		del1T = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1t);
		del2F = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del2f);
		del2T = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del2t);
		del1F = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1f);
		delay1Wet = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Delay1wet);
		delay2Wet = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Delay2wet);
		endd1 = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Endd1);
		qq2 = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Qq2);
		rootnote = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Rootnote);
		speed = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Speed);
		startt1 = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Startt1);
		synth1A = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1a);
		synth1D = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1d);
		synth1End = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1end);
		synth1Q = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1q);
		synth1R = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1r);
		synth1S = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1s);
		synth1Start = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1start);
		synth1Vol = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1vol);
		synth2A = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2a);
		synth2D = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2d);
		synth2Octave = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2octave);
		synth2R = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2r);
		synth2S = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2s);
		synth2Volume = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2volume);
		synthOctave1 = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synthoctave1);
		timme1 = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Timme1);
		volume = lib.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Volume);

	}
	
	// Update is called once per frame
	void Update () {
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1t, del1T);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del2f, del2F);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del2t, del2T);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1f, del1F);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Delay1wet, delay1Wet);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Delay2wet, delay2Wet);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Endd1, endd1);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Qq2, qq2);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Rootnote, rootnote);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Speed, speed);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Startt1, startt1);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1a, synth1A);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1d, synth1D);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1end, synth1End);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1q, synth1Q);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1r, synth1R);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1s, synth1S);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1start, synth1Start);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1vol, synth1Vol);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2a, synth2A);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2d, synth2D);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2octave, synth2Octave);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2r, synth2R);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2s, synth2S);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2volume, synth2Volume);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synthoctave1, synthOctave1);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Timme1, timme1);
		lib.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Volume, volume);
		
	}
}
