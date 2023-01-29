using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RemoveComponents))]
public class RemoveComponentsEditor : Editor 
{
#if UNITY_EDITOR   
	public override void OnInspectorGUI() 
    {
		base.OnInspectorGUI();
		var removeComponents = target as RemoveComponents;

		if (GUILayout.Button ("Remove Components")) 
        {
            removeComponents.RemoveComponentsInChildren();
		}
	}
#endif
}