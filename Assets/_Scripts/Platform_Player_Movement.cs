using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Make platform the parent of objects in collision with platform
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Undo objects in collision and revert to previous state in hierarchy
        collision.transform.SetParent(null);
    }
}
