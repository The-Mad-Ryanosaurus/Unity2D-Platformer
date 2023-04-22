using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    [SerializeField] private Transform childTransform;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        // Update the parent object's position and rotation based on the child object's transform
        transform.position = childTransform.position + offset;
        transform.rotation = childTransform.rotation;
    }
}
