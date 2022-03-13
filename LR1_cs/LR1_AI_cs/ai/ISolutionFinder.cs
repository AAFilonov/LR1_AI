using System.Collections.Generic;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public interface ISolutionFinder 
    {
        List<State> findMoves(State inititalState, State targetState);
        List<State> openState(State currentState);
    }
}