using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    public Transform target;
    // Bottom left corner position
    [SerializeField]
    public Vector2 minPosition;
    // Top right corner position
    [SerializeField]
    public Vector2 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        // Updates Position of Camera that the script is attached too
        // Matches the Position of the player on the x axis and y axis (explore scene horizontall and vertically)
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);


        //Local Scale
        // Old method for the camera
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        // New method for the camera
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        if(player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y +1, minPosition.y, maxPosition.y);

            
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y +1 , minPosition.y, maxPosition.y);

            
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, targetPosition, offsetSmoothing);
        
    }
}
// public GameObject player;
// //public Transform player;
//   public Vector3 offset;
  
//   void Update () 
//   {
//       transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
//   }
// }
