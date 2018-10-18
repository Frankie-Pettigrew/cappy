using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ViewCone))]
public class FieldOfViewEditor : Editor {
	private void OnSceneGUI()
	{
		ViewCone VC = (ViewCone) target;
		Handles.color = Color.white;
		Handles.DrawWireArc(VC.transform.position, Vector3.forward, Vector3.up, 360, VC.viewRadius);
		Vector2 viewAngleA = VC.dirFromAngle(-VC.viewAngle / 2, false);
		Vector2 viewAngleB = VC.dirFromAngle(VC.viewAngle / 2, false);
		
		
		Handles.DrawLine(VC.transform.position, (Vector2) VC.transform.position + viewAngleA * VC.viewRadius);
		Handles.DrawLine(VC.transform.position, (Vector2) VC.transform.position + viewAngleB * VC.viewRadius);
	}
}
