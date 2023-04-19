using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Burst : MonoBehaviour
{

    public ParticleSystem collisionPS;
    public bool once = true;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")&& once){
            
            var emit = collisionPS.emission;
            var dur = collisionPS.duration;

            emit.enabled = true;
            collisionPS.Play();

            once = false;
            
        }
    }
}
