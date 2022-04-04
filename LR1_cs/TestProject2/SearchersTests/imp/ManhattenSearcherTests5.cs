using LR1_AI_cs.ai;

namespace TestProject2.SearchersTests
{
    public class ManhattenSearcherTests5 : AbstractSearcherTests
    {
        public override ISolutionFinder getSearcher()
        {
            return new BoundedManhattenHeuristicSearcherByBest(5);
        }
    }
}