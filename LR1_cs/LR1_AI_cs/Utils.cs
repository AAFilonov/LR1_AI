using System.Collections.Generic;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs
{
    public class Utils
    {
        public static bool contains(IEnumerable<State> enumerable, State stateToCheck)
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
    }
}