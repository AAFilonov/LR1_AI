using LR1_AI_cs.ai;

namespace TestProject2.SearchersTests
{
    public class IterativeInDepthSearcherTests : AbstractSearcherTests
    {
        public override ISolutionFinder getSearcher()
        {
            return new IterativeBoundedInDepthSearchSearcher();
        }
    }
}