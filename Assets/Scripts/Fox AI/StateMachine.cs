using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    //variables for movement
    private float moveSpeed = 1f;

    public State currentState;

    public Transform currentWaypoint;
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;

    // Use this for initialization
    private void Start () {
        //set the state SetState(new ReturnHomeState(this));
        //set the class to the wander state
        SetState(new Wander(this));
    }

    void Update () {
        currentState.Tick();
	}

    public void SetState(State state)
    {
        if (currentState != null)
        {
            //exit out of the state
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "Cube - " + state.GetType().Name;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void MoveToward(Vector3 destination)
    {
        var direction = GetDirection(destination);
        transform.Translate(direction * Time.deltaTime * moveSpeed);

        print("Moving to location");
    }

    private Vector3 GetDirection(Vector3 destination)
    {
        return (destination - transform.position).normalized;
    }

    public void AssignRandomWaypoint()
    {
        currentWaypoint.position = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
    }

}
