using System;
using UnityEngine;

public class RemoveComponents : MonoBehaviour
{
    [SerializeField] private string componentName;
    
    public void RemoveComponentsInChildren()
    {
        foreach (Transform childTransform in transform)
        {
            string fullTypeName = $"{typeof(Component).Namespace}.{componentName},UnityEngine";
            var componentType = Type.GetType(fullTypeName);

            if (componentType == null)
            {
                Debug.LogError($"No component of type '{componentName}' existing in '{typeof(Component).Namespace}'.");
                return;
            }

            var components = childTransform.gameObject.GetComponentsInChildren(componentType);

            if (components.Length <= 0)
            {
                Debug.LogError($"No components of type '{componentName}' found on '{gameObject.name}'.");
                return;
            }

            foreach (var component in components)
            {
                DestroyImmediate(component);
            }

            Debug.Log($"Removed {components.Length} components of type '{componentName}' from '{gameObject.name}'.");
            DestroyImmediate(this);
            break;
        }
    }
}
