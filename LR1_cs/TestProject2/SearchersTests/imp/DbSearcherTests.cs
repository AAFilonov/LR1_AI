using LR1_AI_cs.ai;

namespace TestProject2.SearchersTests
{
    public class DbSearcherTests : AbstractSearcherTests
    {
        public override ISolutionFinder getSearcher()
        {
            return new DbHeuristicSearcher();
        }
    }
}