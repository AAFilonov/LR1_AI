using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public interface ISolutionFinder 
    {
        History find(State inititalState, State targetState);
        Task<History> findAsync(State inititalState, State targetState);
    }
}