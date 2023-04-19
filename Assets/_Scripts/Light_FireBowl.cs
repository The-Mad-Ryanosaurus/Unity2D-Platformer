using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_FireBowl : MonoBehaviour
{

    [SerializeField]
    private Animator Respawn_Bowl;

    [SerializeField]
    private string Respawn_Bowl_Lit = "Respawn_Bowl_Lit";

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
           Respawn_Bowl.Play(Respawn_Bowl_Lit, 0, 0.0f);
        }
    }

}