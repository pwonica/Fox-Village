using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : State {



    public Wander(StateMachine stateMachine) : base(stateMachine) { }

    public override void OnStateEnter()
    {
        stateMachine.AssignRandomWaypoint();
        print("Assigning random waypoint");
    }



    //if you arrived at the point, then go to a new point
    public override void Tick()
    {
        if (ReachedLocation())
        {
            stateMachine.AssignRandomWaypoint();
        }

        stateMachine.MoveToward(stateMachine.currentWaypoint.position);
    }

    private bool ReachedLocation()
    {
        return Vector3.Distance(stateMachine.transform.position, stateMachine.currentWaypoint.position) < 0.5f;
    }
}
