using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (RemoveRagdoll))]
public class RemoveRagdollEditor : Editor 
{
#if UNITY_EDITOR	
	public override void OnInspectorGUI() 
    {
		DrawDefaultInspector();

		RemoveRagdoll removeRagdoll = (RemoveRagdoll) target;

		if (GUILayout.Button ("Remove Ragdoll")) 
        {
			removeRagdoll.RemoveCharacterJoints();
            removeRagdoll.RemoveRigidbodys();
		}
	}
#endif
}