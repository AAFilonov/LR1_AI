using System.Collections.Generic;
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
          public void addState(State stateToAdd)
          {
              _states.Add(new State(stateToAdd));
          }
          public int getHistoryDepth()
          {
              return _states.Count-1;
          }

          public void reset()
          {
              _states.Clear();
          }
    }
}