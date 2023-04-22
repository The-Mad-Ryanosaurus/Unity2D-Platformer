using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{

    [SerializeField]
    private Animator Enemy_Sprite;

    [SerializeField]
    private string Spider_Die = "Spider_Die";

    [SerializeField]
    private GameObject Spider; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "WeakPoint")
        {
            Enemy_Sprite.Play(Spider_Die, 0, 0.0f);

            CapsuleCollider2D capsuleCollider = collision.gameObject.GetComponent<CapsuleCollider2D>();
            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = false;
            }

            PolygonCollider2D polygonCollider = Spider.GetComponent<PolygonCollider2D>();
            if (polygonCollider != null)
            {
                polygonCollider.enabled = false;
            }

            // Destroy the WeakPoint after 0.5 seconds
            Destroy(collision.gameObject, 1.0f);
        }
    }

}