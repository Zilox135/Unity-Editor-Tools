using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RemoveColliders))]  
public class RemoveCollidersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var removeColliders = target as RemoveColliders;  

        if (GUILayout.Button("Remove Child Colliders"))
        {
            removeColliders.RemoveChildColliders();
        }
    }
}
