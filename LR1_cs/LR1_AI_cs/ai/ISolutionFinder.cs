using System.Collections.Generic;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public interface ISolutionFinder 
    {
        List<State> find(State inititalState, State targetState);
        Task<List<State>> findAsync(State inititalState, State targetState);
    }
}