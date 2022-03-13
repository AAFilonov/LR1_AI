using System;
using System.Collections.Generic;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs
{
    public class Utils
    {
        public static bool containsValue(IEnumerable<State> enumerable, State stateToCheck)
        {
            foreach (var state in enumerable)
            {
                if (state.Equals(stateToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool containsValue(IEnumerable<Tuple<int,State>> openQueue, State stateToCheck)
        {
            foreach (var pair in openQueue)
            {
                if (pair.Item2.Equals(stateToCheck))
                {
                    return true;
                }
            }

            return false;
        }
    }
}