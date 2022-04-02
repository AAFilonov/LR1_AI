using System;
using System.Collections.Generic;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs
{
    public class StateRandomizer
    {
        static Random rand = new Random();

        public static State generate(State initialState, int depth)
        {
            State currentState = new State(initialState);
            List<State> history = new List<State>();

            while (depth > 0)
            {
                history.Add(currentState);
                var childs = Game.getAllChildren(currentState);
                if (childs.Count == 0)
                    break;
                childs.RemoveAll(state => Utils.containsValue(history, state));


                int index = rand.Next(0, childs.Count);
                currentState = childs[index];
                depth--;
            }

            return currentState;
        }
    }
}