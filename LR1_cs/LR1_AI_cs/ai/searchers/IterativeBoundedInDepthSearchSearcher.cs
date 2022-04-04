using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class IterativeBoundedInDepthSearchSearcher : AbstractSolutionSearcher
    {
        public override List<State> findMoves(State inititalState, State targetState)
        {
            int depth = 1;
            while (true)
            {
                BoundedInDepthSearchSearcher searcher = new BoundedInDepthSearchSearcher(depth);
                var result = searcher.findMoves(inititalState, targetState);
                if (result.Count == 0)
                {
                    depth++;
                    continue;
                }

                updateStatisitcs(searcher.countClosed, searcher.countOpen, searcher.iterationsCount);
                return result;
            }

            //No solution
            return new List<State>();
        }
    }
}