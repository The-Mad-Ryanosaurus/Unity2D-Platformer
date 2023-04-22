using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private Animator Player_Sprite;

    [SerializeField]
    private string Player_Die = "Player_Die";

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
           Player_Sprite.Play(Player_Die, 0, 0.0f);
        }
    }
    
}
