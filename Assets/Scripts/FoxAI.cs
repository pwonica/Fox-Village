using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behavior
{
    idle, wander, moveToFood
}


public class FoxAI : MonoBehaviour {

    public Behavior currentState = Behavior.idle;
    private Behavior nextState = Behavior.idle;
    private Transform foodTarget;

    public float moveSpeed;
    public bool hasTarget;
    public Transform objectTarget;

    private Rigidbody rigidBody;

    private bool isMoving;
    private Vector3 moveDirection;
    private float distance;






    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        if (hasTarget)
        {
            //rigidBody.velocity = new Vector3(Random.Range(-1f, 1f) * moveSpeed, 0f, Random.Range(-1f, 1f) * moveSpeed);
        }
        else
        {
            rigidBody.velocity = new Vector3(Random.Range(-1f, 1f) * moveSpeed, 0f, Random.Range(-1f, 1f) * moveSpeed);
        }

     

    }


}
