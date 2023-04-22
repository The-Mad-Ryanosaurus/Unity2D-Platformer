using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float moveSpeed;
    public int patrolDestination;

    void Update()
    {
        if(patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            transform.localScale = new Vector2(-1.0f, 1.0f);

            // CALCULATES DISTANCE BETWEEN THE 2 POINTS AND IF THE DISTANCE REACHES ZERO HE PATROLS BACK TO THE OTHER PATROL POINT
            if(Vector2.Distance(transform.position, patrolPoints[0].position) < .2f )
            {
                patrolDestination = 1;
            }
        }
         if(patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            transform.localScale = new Vector2(1.0f, 1.0f);
            // CALCULATES DISTANCE BETWEEN THE 2 POINTS AND IF THE DISTANCE REACHES ZERO HE PATROLS BACK TO THE OTHER PATROL POINT
            if(Vector2.Distance(transform.position, patrolPoints[1].position) < .2f )
            {
                patrolDestination = 0;
            }
        }
    }
}
