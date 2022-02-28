using System.Collections.Generic;
using System.Linq;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs
{
    public class History
    {
          private List<State> _states { get; } = new List<State>();

          public State getState(int index)
          {
              return _states[index];
          }
          public State getLastState()
          {
              return _states.Last();
          }
          public void addState(State stateToAdd)
          {
              _states.Add(new State(stateToAdd));
          }
          public int getHistoryDepth()
          {
              return _states.Count-1;
          }

          public void reverse()
          {
              _states.Reverse();
          }
          public void reset()
          {
              _states.Clear();
          }

          public bool isEmpty()
          {
              return getHistoryDepth() == -1;
          }
    }
}