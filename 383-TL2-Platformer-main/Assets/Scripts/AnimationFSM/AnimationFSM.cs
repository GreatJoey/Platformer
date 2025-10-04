using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AnimationFSM 
{
  public struct Conditions
  {
    public bool isOnGround;
    // public Vector2Int moveDirection;
    public bool movingY;
    public bool movingX;
  }

  public abstract class State
  {
    public string animationName;

    protected State(string animationName)
    {
      this.animationName = animationName;
    }

    public abstract bool IsMatchingConditions(Conditions conditions);
  }

  public class FSM
  {
    public Conditions conditions;
    public State currentState;
    List<State> states = new List<State>();

    public void AddState(State state)
    {
      states.Add(state);
    }

    public void Update() 
    {
      foreach(var state in states)
      {
        if(state.IsMatchingConditions(conditions))
        {
          // Debug.Log("Current state " + state); 
          currentState = state;
          break;
        }
      }
    }
  }
}
