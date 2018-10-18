/**
 * Copyright (c) 2018 Enzien Audio, Ltd.
 * 
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions, and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the phrase "powered by heavy",
 *    the heavy logo, and a hyperlink to https://enzienaudio.com, all in a visible
 *    form.
 * 
 *   2.1 If the Application is distributed in a store system (for example,
 *       the Apple "App Store" or "Google Play"), the phrase "powered by heavy"
 *       shall be included in the app description or the copyright text as well as
 *       the in the app itself. The heavy logo will shall be visible in the app
 *       itself as well.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;
using AOT;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Hv_pedSynths_AudioLib))]
public class Hv_pedSynths_Editor : Editor {

  [MenuItem("Heavy/pedSynths")]
  static void CreateHv_pedSynths() {
    GameObject target = Selection.activeGameObject;
    if (target != null) {
      target.AddComponent<Hv_pedSynths_AudioLib>();
    }
  }

  private Hv_pedSynths_AudioLib _dsp;

  private void OnEnable() {
    _dsp = target as Hv_pedSynths_AudioLib;
  }

  public override void OnInspectorGUI() {
    bool isEnabled = _dsp.IsInstantiated();
    if (!isEnabled) {
      EditorGUILayout.LabelField("Press Play!",  EditorStyles.centeredGreyMiniLabel);
    }
    GUILayout.BeginVertical();
    // EVENTS
    GUI.enabled = isEnabled;
    EditorGUILayout.Space();

    // babang
    if (GUILayout.Button("babang")) {
      _dsp.SendEvent(Hv_pedSynths_AudioLib.Event.Babang);
    }
    // PARAMETERS
    GUI.enabled = true;
    EditorGUILayout.Space();
    EditorGUI.indentLevel++;

    // del1F
    GUILayout.BeginHorizontal();
    float del1F = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1f);
    float newDel1f = EditorGUILayout.Slider("del1F", del1F, 0.1f, 1.0f);
    if (del1F != newDel1f) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1f, newDel1f);
    }
    GUILayout.EndHorizontal();

    // del1T
    GUILayout.BeginHorizontal();
    float del1T = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1t);
    float newDel1t = EditorGUILayout.Slider("del1T", del1T, 10.0f, 2000.0f);
    if (del1T != newDel1t) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1t, newDel1t);
    }
    GUILayout.EndHorizontal();

    // del2F
    GUILayout.BeginHorizontal();
    float del2F = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del2f);
    float newDel2f = EditorGUILayout.Slider("del2F", del2F, 0.1f, 1.0f);
    if (del2F != newDel2f) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del2f, newDel2f);
    }
    GUILayout.EndHorizontal();

    // del2T
    GUILayout.BeginHorizontal();
    float del2T = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del2t);
    float newDel2t = EditorGUILayout.Slider("del2T", del2T, 10.0f, 2000.0f);
    if (del2T != newDel2t) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del2t, newDel2t);
    }
    GUILayout.EndHorizontal();

    // delay1Wet
    GUILayout.BeginHorizontal();
    float delay1Wet = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Delay1wet);
    float newDelay1wet = EditorGUILayout.Slider("delay1Wet", delay1Wet, 0.0f, 1.0f);
    if (delay1Wet != newDelay1wet) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Delay1wet, newDelay1wet);
    }
    GUILayout.EndHorizontal();

    // delay2Wet
    GUILayout.BeginHorizontal();
    float delay2Wet = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Delay2wet);
    float newDelay2wet = EditorGUILayout.Slider("delay2Wet", delay2Wet, 0.0f, 1.0f);
    if (delay2Wet != newDelay2wet) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Delay2wet, newDelay2wet);
    }
    GUILayout.EndHorizontal();

    // endd1
    GUILayout.BeginHorizontal();
    float endd1 = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Endd1);
    float newEndd1 = EditorGUILayout.Slider("endd1", endd1, 20.0f, 20000.0f);
    if (endd1 != newEndd1) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Endd1, newEndd1);
    }
    GUILayout.EndHorizontal();

    // qq2
    GUILayout.BeginHorizontal();
    float qq2 = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Qq2);
    float newQq2 = EditorGUILayout.Slider("qq2", qq2, 1.0f, 256.0f);
    if (qq2 != newQq2) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Qq2, newQq2);
    }
    GUILayout.EndHorizontal();

    // rootNote
    GUILayout.BeginHorizontal();
    float rootNote = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Rootnote);
    float newRootnote = EditorGUILayout.Slider("rootNote", rootNote, 0.0f, 127.0f);
    if (rootNote != newRootnote) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Rootnote, newRootnote);
    }
    GUILayout.EndHorizontal();

    // speed
    GUILayout.BeginHorizontal();
    float speed = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Speed);
    float newSpeed = EditorGUILayout.Slider("speed", speed, 0.0f, 4.0f);
    if (speed != newSpeed) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Speed, newSpeed);
    }
    GUILayout.EndHorizontal();

    // startt1
    GUILayout.BeginHorizontal();
    float startt1 = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Startt1);
    float newStartt1 = EditorGUILayout.Slider("startt1", startt1, 20.0f, 20000.0f);
    if (startt1 != newStartt1) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Startt1, newStartt1);
    }
    GUILayout.EndHorizontal();

    // synth1A
    GUILayout.BeginHorizontal();
    float synth1A = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1a);
    float newSynth1a = EditorGUILayout.Slider("synth1A", synth1A, 10.0f, 10010.0f);
    if (synth1A != newSynth1a) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1a, newSynth1a);
    }
    GUILayout.EndHorizontal();

    // synth1D
    GUILayout.BeginHorizontal();
    float synth1D = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1d);
    float newSynth1d = EditorGUILayout.Slider("synth1D", synth1D, 10.0f, 10010.0f);
    if (synth1D != newSynth1d) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1d, newSynth1d);
    }
    GUILayout.EndHorizontal();

    // synth1End
    GUILayout.BeginHorizontal();
    float synth1End = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1end);
    float newSynth1end = EditorGUILayout.Slider("synth1End", synth1End, 20.0f, 20000.0f);
    if (synth1End != newSynth1end) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1end, newSynth1end);
    }
    GUILayout.EndHorizontal();

    // synth1Q
    GUILayout.BeginHorizontal();
    float synth1Q = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1q);
    float newSynth1q = EditorGUILayout.Slider("synth1Q", synth1Q, 0.0f, 15.0f);
    if (synth1Q != newSynth1q) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1q, newSynth1q);
    }
    GUILayout.EndHorizontal();

    // synth1R
    GUILayout.BeginHorizontal();
    float synth1R = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1r);
    float newSynth1r = EditorGUILayout.Slider("synth1R", synth1R, 10.0f, 10010.0f);
    if (synth1R != newSynth1r) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1r, newSynth1r);
    }
    GUILayout.EndHorizontal();

    // synth1S
    GUILayout.BeginHorizontal();
    float synth1S = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1s);
    float newSynth1s = EditorGUILayout.Slider("synth1S", synth1S, 0.1f, 1.1f);
    if (synth1S != newSynth1s) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1s, newSynth1s);
    }
    GUILayout.EndHorizontal();

    // synth1Start
    GUILayout.BeginHorizontal();
    float synth1Start = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1start);
    float newSynth1start = EditorGUILayout.Slider("synth1Start", synth1Start, 20.0f, 20000.0f);
    if (synth1Start != newSynth1start) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1start, newSynth1start);
    }
    GUILayout.EndHorizontal();

    // synth1Vol
    GUILayout.BeginHorizontal();
    float synth1Vol = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1vol);
    float newSynth1vol = EditorGUILayout.Slider("synth1Vol", synth1Vol, 0.0f, 1.0f);
    if (synth1Vol != newSynth1vol) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth1vol, newSynth1vol);
    }
    GUILayout.EndHorizontal();

    // synth2A
    GUILayout.BeginHorizontal();
    float synth2A = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2a);
    float newSynth2a = EditorGUILayout.Slider("synth2A", synth2A, 10.0f, 10010.0f);
    if (synth2A != newSynth2a) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2a, newSynth2a);
    }
    GUILayout.EndHorizontal();

    // synth2D
    GUILayout.BeginHorizontal();
    float synth2D = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2d);
    float newSynth2d = EditorGUILayout.Slider("synth2D", synth2D, 10.0f, 10010.0f);
    if (synth2D != newSynth2d) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2d, newSynth2d);
    }
    GUILayout.EndHorizontal();

    // synth2Octave
    GUILayout.BeginHorizontal();
    float synth2Octave = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2octave);
    float newSynth2octave = EditorGUILayout.Slider("synth2Octave", synth2Octave, 0.0f, 2.0f);
    if (synth2Octave != newSynth2octave) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2octave, newSynth2octave);
    }
    GUILayout.EndHorizontal();

    // synth2R
    GUILayout.BeginHorizontal();
    float synth2R = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2r);
    float newSynth2r = EditorGUILayout.Slider("synth2R", synth2R, 10.0f, 10010.0f);
    if (synth2R != newSynth2r) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2r, newSynth2r);
    }
    GUILayout.EndHorizontal();

    // synth2S
    GUILayout.BeginHorizontal();
    float synth2S = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2s);
    float newSynth2s = EditorGUILayout.Slider("synth2S", synth2S, 0.1f, 1.1f);
    if (synth2S != newSynth2s) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2s, newSynth2s);
    }
    GUILayout.EndHorizontal();

    // synth2Volume
    GUILayout.BeginHorizontal();
    float synth2Volume = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2volume);
    float newSynth2volume = EditorGUILayout.Slider("synth2Volume", synth2Volume, 0.0f, 1.0f);
    if (synth2Volume != newSynth2volume) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synth2volume, newSynth2volume);
    }
    GUILayout.EndHorizontal();

    // synthOctave1
    GUILayout.BeginHorizontal();
    float synthOctave1 = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synthoctave1);
    float newSynthoctave1 = EditorGUILayout.Slider("synthOctave1", synthOctave1, 0.0f, 2.0f);
    if (synthOctave1 != newSynthoctave1) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Synthoctave1, newSynthoctave1);
    }
    GUILayout.EndHorizontal();

    // timme1
    GUILayout.BeginHorizontal();
    float timme1 = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Timme1);
    float newTimme1 = EditorGUILayout.Slider("timme1", timme1, 10.0f, 10000.0f);
    if (timme1 != newTimme1) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Timme1, newTimme1);
    }
    GUILayout.EndHorizontal();

    // volume
    GUILayout.BeginHorizontal();
    float volume = _dsp.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Volume);
    float newVolume = EditorGUILayout.Slider("volume", volume, 0.0f, 1.0f);
    if (volume != newVolume) {
      _dsp.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Volume, newVolume);
    }
    GUILayout.EndHorizontal();

    EditorGUI.indentLevel--;

    

    GUILayout.EndVertical();
  }
}
#endif // UNITY_EDITOR

[RequireComponent (typeof (AudioSource))]
public class Hv_pedSynths_AudioLib : MonoBehaviour {
  
  // Events are used to trigger bangs in the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_pedSynths_AudioLib script = GetComponent<Hv_pedSynths_AudioLib>();
        script.SendEvent(Hv_pedSynths_AudioLib.Event.Babang);
    }
  */
  public enum Event : uint {
    Babang = 0x4A634528,
  }
  
  // Parameters are used to send float messages into the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_pedSynths_AudioLib script = GetComponent<Hv_pedSynths_AudioLib>();
        // Get and set a parameter
        float del1F = script.GetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1f);
        script.SetFloatParameter(Hv_pedSynths_AudioLib.Parameter.Del1f, del1F + 0.1f);
    }
  */
  public enum Parameter : uint {
    Del1f = 0xF5825AD9,
    Del1t = 0x421996CD,
    Del2f = 0x5339CAF1,
    Del2t = 0xBB24CB61,
    Delay1wet = 0xB4EBE641,
    Delay2wet = 0xBD6EAC55,
    Endd1 = 0xB384B23B,
    Qq2 = 0xD1445317,
    Rootnote = 0x9AC33B76,
    Speed = 0x29E4A0EF,
    Startt1 = 0xE09C5304,
    Synth1a = 0x1539F957,
    Synth1d = 0xDDDACBBA,
    Synth1end = 0x4A13EC31,
    Synth1q = 0xDB64B36E,
    Synth1r = 0xA96418D6,
    Synth1s = 0xE988F806,
    Synth1start = 0x3B05A942,
    Synth1vol = 0xA6A21FE5,
    Synth2a = 0x785CE7F8,
    Synth2d = 0xFC7DB1D0,
    Synth2octave = 0x53517EB1,
    Synth2r = 0x6DF4B46,
    Synth2s = 0xFA226737,
    Synth2volume = 0xB843CEE0,
    Synthoctave1 = 0xC1BA1E58,
    Timme1 = 0x775EB2C7,
    Volume = 0xB1642755,
  }
  
  // Delegate method for receiving float messages from the patch context (thread-safe).
  // Example usage:
  /*
    void Start () {
        Hv_pedSynths_AudioLib script = GetComponent<Hv_pedSynths_AudioLib>();
        script.RegisterSendHook();
        script.FloatReceivedCallback += OnFloatMessage;
    }

    void OnFloatMessage(Hv_pedSynths_AudioLib.FloatMessage message) {
        Debug.Log(message.receiverName + ": " + message.value);
    }
  */
  public class FloatMessage {
    public string receiverName;
    public float value;

    public FloatMessage(string name, float x) {
      receiverName = name;
      value = x;
    }
  }
  public delegate void FloatMessageReceived(FloatMessage message);
  public FloatMessageReceived FloatReceivedCallback;
  public float del1F = 0.6f;
  public float del1T = 150.0f;
  public float del2F = 0.6f;
  public float del2T = 150.0f;
  public float delay1Wet = 0.5f;
  public float delay2Wet = 0.5f;
  public float endd1 = 200.0f;
  public float qq2 = 12.0f;
  public float rootNote = 0.0f;
  public float speed = 2.0f;
  public float startt1 = 2000.0f;
  public float synth1A = 60.0f;
  public float synth1D = 200.0f;
  public float synth1End = 300.0f;
  public float synth1Q = 0.1f;
  public float synth1R = 500.0f;
  public float synth1S = 0.2f;
  public float synth1Start = 2000.0f;
  public float synth1Vol = 0.5f;
  public float synth2A = 200.0f;
  public float synth2D = 200.0f;
  public float synth2Octave = 2.0f;
  public float synth2R = 100.0f;
  public float synth2S = 0.15f;
  public float synth2Volume = 0.5f;
  public float synthOctave1 = 1.0f;
  public float timme1 = 2000.0f;
  public float volume = 0.5f;

  // internal state
  private Hv_pedSynths_Context _context;

  public bool IsInstantiated() {
    return (_context != null);
  }

  public void RegisterSendHook() {
    _context.RegisterSendHook();
  }
  
  // see Hv_pedSynths_AudioLib.Event for definitions
  public void SendEvent(Hv_pedSynths_AudioLib.Event e) {
    if (IsInstantiated()) _context.SendBangToReceiver((uint) e);
  }
  
  // see Hv_pedSynths_AudioLib.Parameter for definitions
  public float GetFloatParameter(Hv_pedSynths_AudioLib.Parameter param) {
    switch (param) {
      case Parameter.Del1f: return del1F;
      case Parameter.Del1t: return del1T;
      case Parameter.Del2f: return del2F;
      case Parameter.Del2t: return del2T;
      case Parameter.Delay1wet: return delay1Wet;
      case Parameter.Delay2wet: return delay2Wet;
      case Parameter.Endd1: return endd1;
      case Parameter.Qq2: return qq2;
      case Parameter.Rootnote: return rootNote;
      case Parameter.Speed: return speed;
      case Parameter.Startt1: return startt1;
      case Parameter.Synth1a: return synth1A;
      case Parameter.Synth1d: return synth1D;
      case Parameter.Synth1end: return synth1End;
      case Parameter.Synth1q: return synth1Q;
      case Parameter.Synth1r: return synth1R;
      case Parameter.Synth1s: return synth1S;
      case Parameter.Synth1start: return synth1Start;
      case Parameter.Synth1vol: return synth1Vol;
      case Parameter.Synth2a: return synth2A;
      case Parameter.Synth2d: return synth2D;
      case Parameter.Synth2octave: return synth2Octave;
      case Parameter.Synth2r: return synth2R;
      case Parameter.Synth2s: return synth2S;
      case Parameter.Synth2volume: return synth2Volume;
      case Parameter.Synthoctave1: return synthOctave1;
      case Parameter.Timme1: return timme1;
      case Parameter.Volume: return volume;
      default: return 0.0f;
    }
  }

  public void SetFloatParameter(Hv_pedSynths_AudioLib.Parameter param, float x) {
    switch (param) {
      case Parameter.Del1f: {
        x = Mathf.Clamp(x, 0.1f, 1.0f);
        del1F = x;
        break;
      }
      case Parameter.Del1t: {
        x = Mathf.Clamp(x, 10.0f, 2000.0f);
        del1T = x;
        break;
      }
      case Parameter.Del2f: {
        x = Mathf.Clamp(x, 0.1f, 1.0f);
        del2F = x;
        break;
      }
      case Parameter.Del2t: {
        x = Mathf.Clamp(x, 10.0f, 2000.0f);
        del2T = x;
        break;
      }
      case Parameter.Delay1wet: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        delay1Wet = x;
        break;
      }
      case Parameter.Delay2wet: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        delay2Wet = x;
        break;
      }
      case Parameter.Endd1: {
        x = Mathf.Clamp(x, 20.0f, 20000.0f);
        endd1 = x;
        break;
      }
      case Parameter.Qq2: {
        x = Mathf.Clamp(x, 1.0f, 256.0f);
        qq2 = x;
        break;
      }
      case Parameter.Rootnote: {
        x = Mathf.Clamp(x, 0.0f, 127.0f);
        rootNote = x;
        break;
      }
      case Parameter.Speed: {
        x = Mathf.Clamp(x, 0.0f, 4.0f);
        speed = x;
        break;
      }
      case Parameter.Startt1: {
        x = Mathf.Clamp(x, 20.0f, 20000.0f);
        startt1 = x;
        break;
      }
      case Parameter.Synth1a: {
        x = Mathf.Clamp(x, 10.0f, 10010.0f);
        synth1A = x;
        break;
      }
      case Parameter.Synth1d: {
        x = Mathf.Clamp(x, 10.0f, 10010.0f);
        synth1D = x;
        break;
      }
      case Parameter.Synth1end: {
        x = Mathf.Clamp(x, 20.0f, 20000.0f);
        synth1End = x;
        break;
      }
      case Parameter.Synth1q: {
        x = Mathf.Clamp(x, 0.0f, 15.0f);
        synth1Q = x;
        break;
      }
      case Parameter.Synth1r: {
        x = Mathf.Clamp(x, 10.0f, 10010.0f);
        synth1R = x;
        break;
      }
      case Parameter.Synth1s: {
        x = Mathf.Clamp(x, 0.1f, 1.1f);
        synth1S = x;
        break;
      }
      case Parameter.Synth1start: {
        x = Mathf.Clamp(x, 20.0f, 20000.0f);
        synth1Start = x;
        break;
      }
      case Parameter.Synth1vol: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        synth1Vol = x;
        break;
      }
      case Parameter.Synth2a: {
        x = Mathf.Clamp(x, 10.0f, 10010.0f);
        synth2A = x;
        break;
      }
      case Parameter.Synth2d: {
        x = Mathf.Clamp(x, 10.0f, 10010.0f);
        synth2D = x;
        break;
      }
      case Parameter.Synth2octave: {
        x = Mathf.Clamp(x, 0.0f, 2.0f);
        synth2Octave = x;
        break;
      }
      case Parameter.Synth2r: {
        x = Mathf.Clamp(x, 10.0f, 10010.0f);
        synth2R = x;
        break;
      }
      case Parameter.Synth2s: {
        x = Mathf.Clamp(x, 0.1f, 1.1f);
        synth2S = x;
        break;
      }
      case Parameter.Synth2volume: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        synth2Volume = x;
        break;
      }
      case Parameter.Synthoctave1: {
        x = Mathf.Clamp(x, 0.0f, 2.0f);
        synthOctave1 = x;
        break;
      }
      case Parameter.Timme1: {
        x = Mathf.Clamp(x, 10.0f, 10000.0f);
        timme1 = x;
        break;
      }
      case Parameter.Volume: {
        x = Mathf.Clamp(x, 0.0f, 1.0f);
        volume = x;
        break;
      }
      default: return;
    }
    if (IsInstantiated()) _context.SendFloatToReceiver((uint) param, x);
  }
  
  public void SendFloatToReceiver(string receiverName, float x) {
    _context.SendFloatToReceiver(StringToHash(receiverName), x);
  }

  public void FillTableWithMonoAudioClip(string tableName, AudioClip clip) {
    if (clip.channels > 1) {
      Debug.LogWarning("Hv_pedSynths_AudioLib: Only loading first channel of '" +
          clip.name + "' into table '" +
          tableName + "'. Multi-channel files are not supported.");
    }
    float[] buffer = new float[clip.samples]; // copy only the 1st channel
    clip.GetData(buffer, 0);
    _context.FillTableWithFloatBuffer(StringToHash(tableName), buffer);
  }

  public void FillTableWithMonoAudioClip(uint tableHash, AudioClip clip) {
    if (clip.channels > 1) {
      Debug.LogWarning("Hv_pedSynths_AudioLib: Only loading first channel of '" +
          clip.name + "' into table '" +
          tableHash + "'. Multi-channel files are not supported.");
    }
    float[] buffer = new float[clip.samples]; // copy only the 1st channel
    clip.GetData(buffer, 0);
    _context.FillTableWithFloatBuffer(tableHash, buffer);
  }

  public void FillTableWithFloatBuffer(string tableName, float[] buffer) {
    _context.FillTableWithFloatBuffer(StringToHash(tableName), buffer);
  }

  public void FillTableWithFloatBuffer(uint tableHash, float[] buffer) {
    _context.FillTableWithFloatBuffer(tableHash, buffer);
  }

  public uint StringToHash(string str) {
    return _context.StringToHash(str);
  }

  private void Awake() {
    _context = new Hv_pedSynths_Context((double) AudioSettings.outputSampleRate);
    
  }
  
  private void Start() {
    _context.SendFloatToReceiver((uint) Parameter.Del1f, del1F);
    _context.SendFloatToReceiver((uint) Parameter.Del1t, del1T);
    _context.SendFloatToReceiver((uint) Parameter.Del2f, del2F);
    _context.SendFloatToReceiver((uint) Parameter.Del2t, del2T);
    _context.SendFloatToReceiver((uint) Parameter.Delay1wet, delay1Wet);
    _context.SendFloatToReceiver((uint) Parameter.Delay2wet, delay2Wet);
    _context.SendFloatToReceiver((uint) Parameter.Endd1, endd1);
    _context.SendFloatToReceiver((uint) Parameter.Qq2, qq2);
    _context.SendFloatToReceiver((uint) Parameter.Rootnote, rootNote);
    _context.SendFloatToReceiver((uint) Parameter.Speed, speed);
    _context.SendFloatToReceiver((uint) Parameter.Startt1, startt1);
    _context.SendFloatToReceiver((uint) Parameter.Synth1a, synth1A);
    _context.SendFloatToReceiver((uint) Parameter.Synth1d, synth1D);
    _context.SendFloatToReceiver((uint) Parameter.Synth1end, synth1End);
    _context.SendFloatToReceiver((uint) Parameter.Synth1q, synth1Q);
    _context.SendFloatToReceiver((uint) Parameter.Synth1r, synth1R);
    _context.SendFloatToReceiver((uint) Parameter.Synth1s, synth1S);
    _context.SendFloatToReceiver((uint) Parameter.Synth1start, synth1Start);
    _context.SendFloatToReceiver((uint) Parameter.Synth1vol, synth1Vol);
    _context.SendFloatToReceiver((uint) Parameter.Synth2a, synth2A);
    _context.SendFloatToReceiver((uint) Parameter.Synth2d, synth2D);
    _context.SendFloatToReceiver((uint) Parameter.Synth2octave, synth2Octave);
    _context.SendFloatToReceiver((uint) Parameter.Synth2r, synth2R);
    _context.SendFloatToReceiver((uint) Parameter.Synth2s, synth2S);
    _context.SendFloatToReceiver((uint) Parameter.Synth2volume, synth2Volume);
    _context.SendFloatToReceiver((uint) Parameter.Synthoctave1, synthOctave1);
    _context.SendFloatToReceiver((uint) Parameter.Timme1, timme1);
    _context.SendFloatToReceiver((uint) Parameter.Volume, volume);
  }
  
  private void Update() {
    // retreive sent messages
    if (_context.IsSendHookRegistered()) {
      Hv_pedSynths_AudioLib.FloatMessage tempMessage;
      while ((tempMessage = _context.msgQueue.GetNextMessage()) != null) {
        FloatReceivedCallback(tempMessage);
      }
    }
  }

  private void OnAudioFilterRead(float[] buffer, int numChannels) {
    Assert.AreEqual(numChannels, _context.GetNumOutputChannels()); // invalid channel configuration
    _context.Process(buffer, buffer.Length / numChannels); // process dsp
  }
}

class Hv_pedSynths_Context {

#if UNITY_IOS && !UNITY_EDITOR
  private const string _dllName = "__Internal";
#else
  private const string _dllName = "Hv_pedSynths_AudioLib";
#endif

  // Thread-safe message queue
  public class SendMessageQueue {
    private readonly object _msgQueueSync = new object();
    private readonly Queue<Hv_pedSynths_AudioLib.FloatMessage> _msgQueue = new Queue<Hv_pedSynths_AudioLib.FloatMessage>();

    public Hv_pedSynths_AudioLib.FloatMessage GetNextMessage() {
      lock (_msgQueueSync) {
        return (_msgQueue.Count != 0) ? _msgQueue.Dequeue() : null;
      }
    }

    public void AddMessage(string receiverName, float value) {
      Hv_pedSynths_AudioLib.FloatMessage msg = new Hv_pedSynths_AudioLib.FloatMessage(receiverName, value);
      lock (_msgQueueSync) {
        _msgQueue.Enqueue(msg);
      }
    }
  }

  public readonly SendMessageQueue msgQueue = new SendMessageQueue();
  private readonly GCHandle gch;
  private readonly IntPtr _context; // handle into unmanaged memory
  private SendHook _sendHook = null;

  [DllImport (_dllName)]
  private static extern IntPtr hv_pedSynths_new_with_options(double sampleRate, int poolKb, int inQueueKb, int outQueueKb);

  [DllImport (_dllName)]
  private static extern int hv_processInlineInterleaved(IntPtr ctx,
      [In] float[] inBuffer, [Out] float[] outBuffer, int numSamples);

  [DllImport (_dllName)]
  private static extern void hv_delete(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern double hv_getSampleRate(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern int hv_getNumInputChannels(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern int hv_getNumOutputChannels(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern void hv_setSendHook(IntPtr ctx, SendHook sendHook);

  [DllImport (_dllName)]
  private static extern void hv_setPrintHook(IntPtr ctx, PrintHook printHook);

  [DllImport (_dllName)]
  private static extern int hv_setUserData(IntPtr ctx, IntPtr userData);

  [DllImport (_dllName)]
  private static extern IntPtr hv_getUserData(IntPtr ctx);

  [DllImport (_dllName)]
  private static extern void hv_sendBangToReceiver(IntPtr ctx, uint receiverHash);

  [DllImport (_dllName)]
  private static extern void hv_sendFloatToReceiver(IntPtr ctx, uint receiverHash, float x);

  [DllImport (_dllName)]
  private static extern uint hv_msg_getTimestamp(IntPtr message);

  [DllImport (_dllName)]
  private static extern bool hv_msg_hasFormat(IntPtr message, string format);

  [DllImport (_dllName)]
  private static extern float hv_msg_getFloat(IntPtr message, int index);

  [DllImport (_dllName)]
  private static extern bool hv_table_setLength(IntPtr ctx, uint tableHash, uint newSampleLength);

  [DllImport (_dllName)]
  private static extern IntPtr hv_table_getBuffer(IntPtr ctx, uint tableHash);

  [DllImport (_dllName)]
  private static extern float hv_samplesToMilliseconds(IntPtr ctx, uint numSamples);

  [DllImport (_dllName)]
  private static extern uint hv_stringToHash(string s);

  private delegate void PrintHook(IntPtr context, string printName, string str, IntPtr message);

  private delegate void SendHook(IntPtr context, string sendName, uint sendHash, IntPtr message);

  public Hv_pedSynths_Context(double sampleRate, int poolKb=10, int inQueueKb=28, int outQueueKb=2) {
    gch = GCHandle.Alloc(msgQueue);
    _context = hv_pedSynths_new_with_options(sampleRate, poolKb, inQueueKb, outQueueKb);
    hv_setPrintHook(_context, new PrintHook(OnPrint));
    hv_setUserData(_context, GCHandle.ToIntPtr(gch));
  }

  ~Hv_pedSynths_Context() {
    hv_delete(_context);
    GC.KeepAlive(_context);
    GC.KeepAlive(_sendHook);
    gch.Free();
  }

  public void RegisterSendHook() {
    // Note: send hook functionality only applies to messages containing a single float value
    if (_sendHook == null) {
      _sendHook = new SendHook(OnMessageSent);
      hv_setSendHook(_context, _sendHook);
    }
  }

  public bool IsSendHookRegistered() {
    return (_sendHook != null);
  }

  public double GetSampleRate() {
    return hv_getSampleRate(_context);
  }

  public int GetNumInputChannels() {
    return hv_getNumInputChannels(_context);
  }

  public int GetNumOutputChannels() {
    return hv_getNumOutputChannels(_context);
  }

  public void SendBangToReceiver(uint receiverHash) {
    hv_sendBangToReceiver(_context, receiverHash);
  }

  public void SendFloatToReceiver(uint receiverHash, float x) {
    hv_sendFloatToReceiver(_context, receiverHash, x);
  }

  public void FillTableWithFloatBuffer(uint tableHash, float[] buffer) {
    if (hv_table_getBuffer(_context, tableHash) != IntPtr.Zero) {
      hv_table_setLength(_context, tableHash, (uint) buffer.Length);
      Marshal.Copy(buffer, 0, hv_table_getBuffer(_context, tableHash), buffer.Length);
    } else {
      Debug.Log(string.Format("Table '{0}' doesn't exist in the patch context.", tableHash));
    }
  }

  public uint StringToHash(string s) {
    return hv_stringToHash(s);
  }

  public int Process(float[] buffer, int numFrames) {
    return hv_processInlineInterleaved(_context, buffer, buffer, numFrames);
  }

  [MonoPInvokeCallback(typeof(PrintHook))]
  private static void OnPrint(IntPtr context, string printName, string str, IntPtr message) {
    float timeInSecs = hv_samplesToMilliseconds(context, hv_msg_getTimestamp(message)) / 1000.0f;
    Debug.Log(string.Format("{0} [{1:0.000}]: {2}", printName, timeInSecs, str));
  }

  [MonoPInvokeCallback(typeof(SendHook))]
  private static void OnMessageSent(IntPtr context, string sendName, uint sendHash, IntPtr message) {
    if (hv_msg_hasFormat(message, "f")) {
      SendMessageQueue msgQueue = (SendMessageQueue) GCHandle.FromIntPtr(hv_getUserData(context)).Target;
      msgQueue.AddMessage(sendName, hv_msg_getFloat(message, 0));
    }
  }
}
