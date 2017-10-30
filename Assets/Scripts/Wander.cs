using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float moveSpeed;
    public float rotationSpeed;

    public Vector3 waypoint;
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;

   

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        GetWaypoint();
        
        InvokeRepeating("GetWaypoint", 0f, 6f);
    }

    private void Update()
    {
        if (waypoint != null)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(waypoint.position - transform.position), rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 0f, transform.position.z), waypoint, moveSpeed * Time.deltaTime);
            transform.LookAt(waypoint);
        }

        if ((transform.position - waypoint).magnitude < 3)
        {
            GetWaypoint();
        }

        /*
        //check to see if at location
        if ((transform.position - wayPoint).sqrMagnitude < 9)
        {
            Wander();
        }
        */

    }

    private void GetWaypoint()
    {
        waypoint = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
        print(waypoint);
    }

  
 

}
