using UnityEngine;

public class RemoveColliders : MonoBehaviour
{
    public void RemoveChildColliders()
    {
        var colliders = gameObject.GetComponentsInChildren<Collider>();

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                DestroyImmediate(collider);
                DestroyImmediate(this);
            }
        }      
        else
        {
            if (this != null)
            {
                Debug.LogWarning($"No colliders found on {gameObject.name}!");
                DestroyImmediate(this);
                return;
            }
        }
    }
}
