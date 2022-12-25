using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RemoveRagdoll : MonoBehaviour
{
    public void RemoveCharacterJoints()
    {
        foreach (Transform childObject in transform)
        {
            CharacterJoint[] joint = childObject.gameObject.GetComponentsInChildren<CharacterJoint>();
 
            if(joint.Length > 0)
            {                      
                foreach(CharacterJoint characterJoint in joint)
                {
                    DestroyImmediate(characterJoint);
                }
            }
        }
    }

    public void RemoveRigidbodys()
    {
        foreach (Transform childObject in transform)
        {
            Rigidbody[] rb = childObject.gameObject.GetComponentsInChildren<Rigidbody>();
 
            if(rb.Length > 0)
            {                      
                foreach(Rigidbody rigidbody in rb)
                {
                    DestroyImmediate(rigidbody);
                }
            }
        }
    }
}
