using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {

    protected StateMachine stateMachine;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    //when this state is created, assign it to the correct state machine
    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }


}
